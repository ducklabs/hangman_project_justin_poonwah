using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Hangman.Models
{
    public class Game
    {
        private const int MAX_NUMBER_OF_GUESSES = 10;

        private GameResultState _gameResultState;
        private string _correctWord;
        private string _correctGuessedLetters = "", _incorrectGuessedLetters = "";

        public Game()
        {
            this._correctWord = WordGenerator.GenerateRandomWord();
        }

        public GameResult GetGameResult()
        {
            return new GameResult(_gameResultState);
        }

        public void Guess(string guess)
        {
            if (guess.IsNullOrWhiteSpace()) return;
            AddGuess(guess);
            if (GameIsOver())
            {
                EndGame();
                return;
            }
        }

        public GameStatus GetGameStatus()
        {
            return new GameStatus(_correctWord, _correctGuessedLetters, _incorrectGuessedLetters);
        }

        private void EndGame()
        {
            if (HasGuessedAllLettersCorrectly())
            {
                _gameResultState = GameResultState.Won;
            }
            else
            {
                _gameResultState = GameResultState.Lost;
            }
        }

        public bool GameIsOver()
        {
            return HasTooManyIncorrectGuesses() || HasGuessedAllLettersCorrectly();
        }

        private bool HasGuessedAllLettersCorrectly()
        {
            var correctLetterSet = ConvertStringToLetterSet(_correctWord);
            var correctLetterGuessesSet = ConvertStringToLetterSet(_correctGuessedLetters);
            return correctLetterSet.Count.Equals(correctLetterGuessesSet.Count);
        }

        private HashSet<char> ConvertStringToLetterSet(string stringToConvert)
        {
            var letterSet = new HashSet<char>();
            foreach (var letter in stringToConvert.ToLower().ToCharArray())
            {
                letterSet.Add(letter);
            }
            return letterSet;
        }

        private bool HasTooManyIncorrectGuesses()
        {
            return _incorrectGuessedLetters.Length >= MAX_NUMBER_OF_GUESSES;
        }

        private void AddGuess(string guess)
        {
            guess = guess.ToLower();
            var correctLetterSet = ConvertStringToLetterSet(_correctWord);
            if (correctLetterSet.Contains(guess.ToCharArray()[0]))
            {
                _correctGuessedLetters += guess;
            }
            else
            {
                _incorrectGuessedLetters += guess;
            }
        }
    }
}