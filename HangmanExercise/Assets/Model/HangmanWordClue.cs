using System;

/// <summary>
/// A word / clue pair
/// </summary>
public class HangmanWordClue
{
    public HangmanWord HangWord;
    public string Clue;
    public string Theme;

    public HangmanWordClue(string word, string clue, string theme)
    {
        HangWord = new HangmanWord(word);
        Clue = clue;
        Theme = theme;
    }

    private static HangmanWordClue[] possibleWordClues;
    public static HangmanWordClue[] PossibleWordClues
    {
        get
        {
            if (possibleWordClues == null)
                possibleWordClues = new HangmanWordClue[]
                {
                    new HangmanWordClue("Justin", "The developer who is doing this test", "Programming"),
                    new HangmanWordClue("Rabbit", "A small land mammal with long ears", "General"),
                    new HangmanWordClue("Volcano", "In which to sacrifice virgins", "General"),
                    new HangmanWordClue("Kamehameha", "Goku Massive energy attack, also hawaiian king.", "General"),
                    new HangmanWordClue("publish", "Push website to the internet", "General"),
                    new HangmanWordClue("Kravmaga", "Isreali fighting style", "General"),
                    new HangmanWordClue("Trapdoorbear", "Justin's company", "Programming"),
                    new HangmanWordClue("Microsoft", "Richest man in the world founded this company", "General"),
                    new HangmanWordClue("tedious", "Typing words and clues into a text file", "General"),
                    new HangmanWordClue("hangman", "the name of this game", "Programming"),
                    new HangmanWordClue("obsolescence", "Junk stops working due to planned _____", "General"),
                    new HangmanWordClue("developer", "so much more than merely a programmer", "Programming"),
                    new HangmanWordClue("programmer", "traditionally poor at spelling", "Programming"),
                    new HangmanWordClue("Tiberius", "What is captain kirk's middle name?", "Star Trek"),
                    new HangmanWordClue("roddenberry", "Star Trek creator", "Star Trek")
                };
            return possibleWordClues;
        }
    }

    public static HangmanWordClue FindByWord(string word)
    {
        for(int i = 0; i < possibleWordClues.Length; i++)
        {
            if (possibleWordClues[i].HangWord.Word.ToLower().Equals(word.ToLower()))
                return new HangmanWordClue(possibleWordClues[i].HangWord.Word, possibleWordClues[i].Clue, possibleWordClues[i].Theme);
        }
        return possibleWordClues[0];
    }
}

