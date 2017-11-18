using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangman.Models
{
    public class WordGeneratorFactory
    {
        public static WordGenerator Instance = new PreMadeListWordGenerator();
    }


    public interface WordGenerator
    {
        string GenerateNextWord();
    }
}