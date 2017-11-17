using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameResult
    {
        public GameResultState GameResultState { get; set; }

        public GameResult(GameResultState gameResultState)
        {
            GameResultState = gameResultState;
        }
    }

}