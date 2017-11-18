using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class PreMadeListWordGenerator : WordGenerator
    {
        private string[] _possibleWords;
        private string[] PossibleWords
        {
            get
            {
                if (_possibleWords == null)
                    _possibleWords = new string[]
                    {
                        "Justin",
                        "Rabbit",
                        "Volcano",
                        "Kamehameha",
                        "publish", 
                        "Kravmaga", 
                        "Trapdoorbear", 
                        "Microsoft", 
                        "tedious", 
                        "hangman", 
                        "obsolescence",
                        "developer", 
                        "programmer",
                        "Tiberius",
                        "roddenberry"
                    };
                return _possibleWords;
            }
        }

        //internal string GenerateRandomLetter(int length)
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    return new string(Enumerable.Repeat(chars, length)
        //        .Select(s => s[random.Next(s.Length)]).ToArray());
        //}

        private int lastWordIndexUsed = -1;

        public string GenerateNextWord()
        {
            Random random = new Random();
            var wordIndex = random.Next(PossibleWords.Length);
            while (wordIndex == lastWordIndexUsed)
            {
                wordIndex = random.Next(PossibleWords.Length);
            }
            lastWordIndexUsed = wordIndex;
            return PossibleWords[lastWordIndexUsed];
        }
    }
}