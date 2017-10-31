using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class represents a letter within the guessable word.  If the letter is successfully guessed, 
/// it will display the Letter Text.  If not, the letter text will be disabled.
/// </summary>
public class WordLetter : MonoBehaviour
{
    public HangmanLetter Letter;
    public Text LetterText;

    public static WordLetter CreateWordLetter(HangmanLetter letter, Transform parent)
    {
        GameObject buttonPrefab = (GameObject)Resources.Load("Prefabs/WordLetterPrefab");
        GameObject newGO = Instantiate(buttonPrefab);
        WordLetter wl = newGO.GetComponent<WordLetter>();
        wl.Letter = letter;
        wl.Letter.LetterGuessed = wl.SetGuessedState;
        wl.LetterText.text = letter.Letter;
        wl.LetterText.gameObject.SetActive(false);
        wl.transform.parent = parent;

        return wl;
    }

    /// <summary>
    /// If the guessed state of the letter becomes true, it will show the letter text above the underscore.
    /// </summary>
    /// <param name="state"></param>
    public void SetGuessedState(bool state)
    {
        if (state)
            ShowText();
    }

    /// <summary>
    /// Just shows the text above the underscore.
    /// </summary>
    public void ShowText()
    {
        LetterText.gameObject.SetActive(true);
    }
}

