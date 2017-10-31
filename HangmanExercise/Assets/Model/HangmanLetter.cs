using System;

/// <summary>
/// The hangman letter class does most of the heavy lifting.  It's responsible for holding the data for
/// which letters have been guessed, whether or not the guess is incorrect or correct, and whether or
/// not to show the letter above the underscore.
/// 
/// The Hangman word is comprised of these hangman letters, and the alphabet buttons all point to one of these.
/// </summary>
public class HangmanLetter
{
    public string Letter;
    public Action<bool> LetterGuessed;
    private bool? wordContains = null;
    public bool? WordContains
    {
        get { return wordContains; }
        set
        {
            if(value.HasValue)
            {
                LetterGuessed(value.Value);
                wordContains = value;
            }
        }
    }

    public HangmanLetter(string letter)
    {
        Letter = letter;
        WordContains = null;
    }


}
