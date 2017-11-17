using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameResult
    {
        public int StepsIntoTheGallows { get; set; }
        public GameResultState GameResultState { get; set; }
        public string CorrectWord { get; set; }

    public GameResult(GameResultState gameResultState, int stepsIntoTheGallows, string correctWord)
        {
            GameResultState = gameResultState;
            StepsIntoTheGallows = stepsIntoTheGallows;
            CorrectWord = correctWord;
        }
    }

}