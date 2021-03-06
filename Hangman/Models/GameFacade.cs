﻿using System;
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

        public static void Guess(char guess)
        {
            GameRepository.FindCurrentGame().Guess(guess);
        }

        public static void CreateGame()
        {
            if (!IsGameInProgress)
                GameRepository.CreateGame();
        }

        public static void EndGame()
        {
            GameRepository.EndGame();
        }

        public static bool IsGameInProgress
        {
            get
            {
                if (GameRepository.FindCurrentGame() != null)
                    return true;
                return false;
            }
        }

        public static bool IsGameOver
        {
            get
            {
                if (GameRepository.FindCurrentGame() == null)
                    return true;
                if (GameRepository.FindCurrentGame().GameIsOver())
                    return true;
                return false;
            }
        }

        public static GameStatus GetGameStatus()
        {
            return GameRepository.FindCurrentGame().GetGameStatus();
        }

        public static void GuessWholeWord(string wordGuess)
        {
            GameRepository.FindCurrentGame().GuessWholeWord(wordGuess);
        }
    }
}