using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public static class WordGenerator
    {
        public static string GenerateWord(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}