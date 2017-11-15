using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class Game
    {
        GameResult _gameResult = new GameResult();
        private string _correctWord;

        public Game()
        {
            this._correctWord = "A";
        }

        public GameResult GetGameResult()
        {
            return _gameResult;
        }

        public void Guess(string guess)
        {
            if (guess.Equals(_correctWord))
            {
                _gameResult.gamestatus = "Won";
            }
            else
            {
                _gameResult.gamestatus = "Lost";
            }
        }

        public GameStatus GetGameStatus()
        {
            return new GameStatus(_correctWord);
        }
    }
}