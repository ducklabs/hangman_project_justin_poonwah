using System;
using UnityEngine;
using UnityEngine.UI;

public class LetterGuessButton : MonoBehaviour
{
    public HangmanLetter Letter;
    public Text ButtonText;
    public Image LittleRedX;
    public Image GreenCheck;

    public static LetterGuessButton CreateLetterGuessButton(HangmanLetter HL, Transform parent, Action<LetterGuessButton> Click)
    {
        GameObject buttonPrefab = (GameObject)Resources.Load("Prefabs/LetterGuessButtonPrefab");
        GameObject letterObj = Instantiate(buttonPrefab);
        LetterGuessButton lgb = letterObj.GetComponent<LetterGuessButton>();
        lgb.Letter = HL;
        lgb.Letter.LetterGuessed = lgb.SetGuessedState;
        lgb.ButtonText.text = HL.Letter;
        lgb.transform.parent = parent;
        Button b = letterObj.GetComponent<Button>();
        //Setup the onClick listener.
        b.onClick.AddListener(() => { Click(lgb); });

        return lgb;
    }

    public void SetGuessedState(bool state)
    {
        if(state)
            GreenCheck.gameObject.SetActive(true);
        else
            LittleRedX.gameObject.SetActive(true);
        GetComponent<Button>().interactable = false;
    }

    public void SetCorrect()
    {
    }

    public void Reset()
    {
        LittleRedX.gameObject.SetActive(false);
        GreenCheck.gameObject.SetActive(false);
    }
}

