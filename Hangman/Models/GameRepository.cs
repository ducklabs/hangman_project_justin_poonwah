using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public static class GameRepository
    {
        public static Game Game;

        public static Game FindCurrentGame()
        {
            return Game;
        }


        public static void CreateGame()
        {
            Game = new Game();
        }

        public static void EndGame()
        {
            Game = null;
        }
    }
}