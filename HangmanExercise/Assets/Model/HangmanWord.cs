
public class HangmanWord
{
    public string Word;
    public HangmanLetter[] Letters;

    public HangmanWord(string word)
    {
        Word = word;
        Letters = new HangmanLetter[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            Letters[i] = new HangmanLetter(word[i].ToString());
        }
    }
}
