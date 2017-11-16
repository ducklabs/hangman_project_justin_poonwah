using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class Game
    {
        private GameState _gameState;
        private string _correctWord;

        public Game()
        {
            this._correctWord = WordGenerator.GenerateWord(1);
        }

        public GameResult GetGameResult()
        {
            return new GameResult(_gameState);
        }

        public void Guess(string guess)
        {
            if (!String.IsNullOrEmpty(guess))
            {
                if (guess.Equals(_correctWord))
                {
                    _gameState = GameState.Won;
                }
                else
                {
                    _gameState = GameState.Lost;
                }
            }
            else
                _gameState = GameState.Lost;
        }

        public GameStatus GetGameStatus()
        {
            return new GameStatus(_correctWord);
        }
    }
}