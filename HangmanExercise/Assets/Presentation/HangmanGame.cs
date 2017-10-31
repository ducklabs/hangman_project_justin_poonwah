using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The main game presentation is responsible for clearing the buttons and setting up the word underscores
/// when the game is started or reset.
/// </summary>
public class HangmanGame : MonoBehaviour {

    public GameObject pnlLetterButtonArea, pnlWordLetterArea, pnlSuccess, pnlFailed;
    public InputField ifWordGuess;
    public Text lblClue, lblTheme;
    public RawImage HangmanImage;

    public List<LetterGuessButton> LetterButtons = new List<LetterGuessButton>();
    public List<WordLetter> WordLetters = new List<WordLetter>();

    private HangmanGameLogic GameLogic;

    // Use this for initialization
    void Start ()
    {
        GameLogic = new HangmanGameLogic();
        GameLogic.NumGuessesChanged = SetNumGuessesImage;
        NewGameButtonPressed();
    }

    /// <summary>
    /// Sets up all of the letter buttons you can choose from.  The action passed in will update the button to have a 
    /// check mark or a little X if the button is pressed.
    /// </summary>
    public void SetupGuessableLetters()
    {
        //first clear any existing letter buttons.
        for(int i = 0; i < LetterButtons.Count; i++)
        {
            Destroy(LetterButtons[i].gameObject);
        }
        LetterButtons.Clear();

        for(int i = 0; i < GameLogic.GuessableLetters.Length; i++)
        {
            LetterButtons.Add(LetterGuessButton.CreateLetterGuessButton(GameLogic.GuessableLetters[i], pnlLetterButtonArea.transform, MakeLetterGuessButtonPressed));
        }
    }

    /// <summary>
    /// Sets up all of the underscores and hides the letters from the word.
    /// </summary>
    public void SetupWordLetters()
    {
        //Clear any existing word letters.
        for (int i = 0; i < WordLetters.Count; i++)
        {
            Destroy(WordLetters[i].gameObject);
        }
        WordLetters.Clear();
        
        if (GameLogic.GuessableWord.HangWord.Letters.Length > 0)
        {
            for(int i = 0; i < GameLogic.GuessableWord.HangWord.Letters.Length; i++)
            {
                WordLetters.Add(WordLetter.CreateWordLetter(GameLogic.GuessableWord.HangWord.Letters[i], pnlWordLetterArea.transform));
            }
        }

        lblClue.text = GameLogic.GuessableWord.Clue;
        lblTheme.text = GameLogic.GuessableWord.Theme;
    }
    
    public void NewGameButtonPressed()
    {
        pnlSuccess.gameObject.SetActive(false);
        pnlFailed.gameObject.SetActive(false);
        
        GameLogic.ResetGame();
        SetupGuessableLetters();
        SetupWordLetters();
    }

    public void MakeLetterGuessButtonPressed(LetterGuessButton pressedButton)
    {
        if(GameLogic.MakeLetterGuess(pressedButton.ButtonText.text))
            WordGuessed();
        if (GameLogic.NumGuesses == HangmanGameLogic.MAX_GUESSES)
            GuessingFailed();
    }

    public void MakeWordGuessButtonPressed()
    {
        string word = ifWordGuess.text;
        if (!string.IsNullOrEmpty(word))
        {
            if (GameLogic.WordGuessed(word))
                WordGuessed();
            else
            {
                WordGuessFailed();
            }
        }
    }

    /// <summary>
    /// Show the success and play again screen.
    /// </summary>
    public void WordGuessed()
    {
        pnlSuccess.gameObject.SetActive(true);
    }

    /// <summary>
    /// Display the "You failed" screen.
    /// </summary>
    public void GuessingFailed()
    {
        pnlFailed.gameObject.SetActive(true);
    }

    /// <summary>
    /// Show a message that the word the user guessed was not it!
    /// </summary>
    public void WordGuessFailed()
    {
        if (GameLogic.NumGuesses == HangmanGameLogic.MAX_GUESSES)
            GuessingFailed();
    }


    public void ResumeExistingGame()
    {
        pnlSuccess.gameObject.SetActive(false);
        pnlFailed.gameObject.SetActive(false);

        GameLogic.ResetGame();

        GameLogic.LoadSavedWord();
        SetupGuessableLetters();
        SetupWordLetters();
        GameLogic.LoadSavedGuesses();
    }

    public void SaveCurrentGame()
    {
        GameLogic.SaveCurrentGame();
    }

    public void SetNumGuessesImage()
    {
        string imagePath = "Sprites/Hang" + GameLogic.NumGuesses;
        var v = Resources.Load(imagePath);
        Texture2D tex = (Texture2D)v;
        HangmanImage.texture = tex;
    }
}
