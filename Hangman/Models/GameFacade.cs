using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameFacade
    {
        public static GameResult GetGameResult()
        {
            return GameRepository.FindCurrentGame().GetGameResult();
        }

        public static void Guess(string guess)
        {
            GameRepository.FindCurrentGame().Guess(guess);
        }

        public static void CreateGame()
        {
            GameRepository.CreateGame();
        }

        public static GameStatus GetGameStatus()
        {
            return GameRepository.FindCurrentGame().GetGameStatus();
        }
    }
}