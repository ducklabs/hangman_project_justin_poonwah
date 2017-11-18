using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class ConfigurableWordGenerator : WordGenerator
    {
        public string Word = "";

        public string GenerateNextWord()
        {
            return Word;
        }
    }
}