using System;
using System.Collections.Generic;
using UnityEngine;

public class HangmanGameLogic
{
    public const int MAX_GUESSES = 10;

    public HangmanWordClue GuessableWord;
    public HangmanLetter[] GuessableLetters;

    public Action NumGuessesChanged;
    public bool WordHasBeenGuessed;

    private int numGuesses;
    public int NumGuesses
    {
        get { return numGuesses; }
        set
        {
            numGuesses = value;
            NumGuessesChanged();
        }
    }

    /// <summary>
    /// Sets the word to be guessed and clears the guesses.
    /// </summary>
    public void ResetGame()
    {
        ChooseGuessableWordAndClue();
        InitializeAlphabet();
        NumGuesses = 0;
    }

    public void InitializeAlphabet()
    {
        GuessableLetters = new HangmanLetter[26];
        GuessableLetters[0] = new HangmanLetter("A");
        GuessableLetters[1] = new HangmanLetter("B");
        GuessableLetters[2] = new HangmanLetter("C");
        GuessableLetters[3] = new HangmanLetter("D");
        GuessableLetters[4] = new HangmanLetter("E");
        GuessableLetters[5] = new HangmanLetter("F");
        GuessableLetters[6] = new HangmanLetter("G");
        GuessableLetters[7] = new HangmanLetter("H");
        GuessableLetters[8] = new HangmanLetter("I");
        GuessableLetters[9] = new HangmanLetter("J");
        GuessableLetters[10] = new HangmanLetter("K");
        GuessableLetters[11] = new HangmanLetter("L");
        GuessableLetters[12] = new HangmanLetter("M");
        GuessableLetters[13] = new HangmanLetter("N");
        GuessableLetters[14] = new HangmanLetter("O");
        GuessableLetters[15] = new HangmanLetter("P");
        GuessableLetters[16] = new HangmanLetter("Q");
        GuessableLetters[17] = new HangmanLetter("R");
        GuessableLetters[18] = new HangmanLetter("S");
        GuessableLetters[19] = new HangmanLetter("T");
        GuessableLetters[20] = new HangmanLetter("U");
        GuessableLetters[21] = new HangmanLetter("V");
        GuessableLetters[22] = new HangmanLetter("W");
        GuessableLetters[23] = new HangmanLetter("X");
        GuessableLetters[24] = new HangmanLetter("Y");
        GuessableLetters[25] = new HangmanLetter("Z");
    }

    public void ChooseGuessableWordAndClue()
    {
        int wordClueIndex = UnityEngine.Random.Range(0, HangmanWordClue.PossibleWordClues.Length - 1);
        GuessableWord = HangmanWordClue.PossibleWordClues[wordClueIndex];
    }
    
    /// <summary>
    /// Making a letter guess will either uncover all letters within the hangman word or uncover nothing and increase the
    /// number of guesses by one.  If the word has been guessed in it's entirety this method will return true
    /// </summary>
    public bool MakeLetterGuess(string letterGuess)
    {
        bool found = false;
        for(int i = 0; i < GuessableWord.HangWord.Letters.Length; i++)
        {
            if (letterGuess.ToLower().Equals(GuessableWord.HangWord.Letters[i].Letter.ToLower()))
            {
                //If the letter is found in the guessable word, it notifies the front end via the anonymous action.
                GuessableWord.HangWord.Letters[i].WordContains = true;
                //If the letter is found in the guessable word, it notifies the button to show the little check mark.
                SetLetterGuessed(letterGuess, true);
                found = true;
            }
        }
        
        if (!found)
        {
            SetLetterGuessed(letterGuess, false);
            NumGuesses++;
        }
        return WordHasBeenFoundViaLetterGuessing();
    }

    /// <summary>
    /// Finds the letter within the list of possible guessable letters and changes the "Word contains" variable to yes/no.
    /// </summary>
    /// <param name="letter"></param>
    /// <param name="contained"></param>
    public void SetLetterGuessed(string letter, bool contained)
    {
        for(int i = 0; i < GuessableLetters.Length; i++)
        {
            if (letter == GuessableLetters[i].Letter)
                GuessableLetters[i].WordContains = contained;
        }
    }

    /// <summary>
    /// Players can try to guess the entire word.
    /// </summary>
    public bool WordGuessed(string word)
    {
        if (word.ToLower().Equals(GuessableWord.HangWord.Word.ToLower()))
            return true;

        NumGuesses++;
        return false;
    }

    /// <summary>
    /// Determines whether or not the word has been found from letter guessing by looping through the word and making sure each letter
    /// has been guessed.
    /// </summary>
    /// <returns></returns>
    public bool WordHasBeenFoundViaLetterGuessing()
    {
        for(int i = 0; i < GuessableWord.HangWord.Letters.Length; i++)
        {
            if (!(GuessableWord.HangWord.Letters[i].WordContains.HasValue && GuessableWord.HangWord.Letters[i].WordContains.Value == true))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Persists the word and the letters that have been guessed to some cookie.
    /// </summary>
    public void SaveCurrentGame()
    {
        PlayerPrefs.SetString("GuessableWord", GuessableWord.HangWord.Word);
        string lettersGuessed = "";
        /*for (int i = 0; i < GuessableWord.HangWord.Letters.Length; i++)
        {
            if (GuessableWord.HangWord.Letters[i].WordContains.HasValue && GuessableWord.HangWord.Letters[i].WordContains.Value)
                lettersGuessed += GuessableWord.HangWord.Letters[i].Letter;
        }*/
        for(int i = 0; i < GuessableLetters.Length; i++)
        {
            if (GuessableLetters[i].WordContains.HasValue)
                lettersGuessed += GuessableLetters[i].Letter;
        }

        PlayerPrefs.SetString("LettersGuessed", lettersGuessed);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// If the game was saved, this will load the word that the user was trying to guess.
    /// </summary>
    /// <returns></returns>
    public void LoadSavedWord()
    {
        string WordToGuess = PlayerPrefs.GetString("GuessableWord");

        //Find and set the word in the list of word/clues
        GuessableWord = HangmanWordClue.FindByWord(WordToGuess);
        //Set the letters guessed.
        
    }
    
    /// <summary>
    /// If the game was saved, this will load all of the guesses and make each guess.
    /// If there are actions associated with the letters, they will fire and update the front end.
    /// </summary>
    public void LoadSavedGuesses()
    {
        string LettersGuessed = PlayerPrefs.GetString("LettersGuessed");
        for (int i = 0; i < LettersGuessed.Length; i++)
        {
            MakeLetterGuess(LettersGuessed[i].ToString());
        }
    }
}

