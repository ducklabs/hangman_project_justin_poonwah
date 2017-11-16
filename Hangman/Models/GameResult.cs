using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameResult
    {
        public GameState GameState { get; set; }

        public GameResult(GameState gameState)
        {
            GameState = gameState;
        }
    }

}