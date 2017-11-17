using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameStatus
    {
        public string CorrectWord { get; set; }
        public string WordHint { get; set; }
        public string CorrectGuessedLetters { get; set; }
        public string IncorrectGuessedLetters { get; set; }
        public int StepsIntoTheGallows { get; set; }

        public GameStatus(string theCorrectWord, string correctGuessedLetters, string incorrectGuessedLetters, int stepsIntoTheGallows)
        {
            CorrectWord = theCorrectWord;
            CorrectGuessedLetters = correctGuessedLetters;
            IncorrectGuessedLetters = incorrectGuessedLetters;
            StepsIntoTheGallows = stepsIntoTheGallows;
        }
    }
}