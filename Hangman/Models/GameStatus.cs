using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class GameStatus
    {
        public string CorrectWord { get; set; }

        public GameStatus(string theCorrectWord)
        {
            CorrectWord = theCorrectWord;
        }
    }
}