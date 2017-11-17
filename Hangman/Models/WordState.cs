using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class WordState
    {
        public string CorrectWord { get; set; }
        public string GuessedLetters { get; set; }
        
        public WordState(string word, string guessedLetters)
        {
            CorrectWord = word;
            GuessedLetters = guessedLetters;
        }
    }
}