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
        private HashSet<char> _correctGuessedLetters = new HashSet<char>();
        private HashSet<char> _incorrectGuessedLetters = new HashSet<char>();

        public Game()
        {
            this._correctWord = WordGenerator.GenerateRandomWord();
        }

        public GameResult GetGameResult()
        {
            return new GameResult(_gameResultState, GetStepsIntoTheGallow(), _correctWord);
        }

        public void Guess(char guess)
        {
            if (Char.IsWhiteSpace(guess)) return;
            AddGuess(guess);
            if (GameIsOver())
            {
                EndGame();
                return;
            }
        }

        public GameStatus GetGameStatus()
        {
            return new GameStatus(_correctWord, _correctGuessedLetters, _incorrectGuessedLetters, GetStepsIntoTheGallow());
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
            return correctLetterSet.SetEquals(_correctGuessedLetters);
        }

        private HashSet<char> ConvertStringToLetterSet(string stringToConvert)
        {
            return new HashSet<char>(stringToConvert.ToLower().ToCharArray());
        }

        private int GetStepsIntoTheGallow()
        {
            return _incorrectGuessedLetters.Count;
        }

        private bool HasTooManyIncorrectGuesses()
        {
            return _incorrectGuessedLetters.Count >= MAX_NUMBER_OF_GUESSES;
        }

        private void AddGuess(char guess)
        {
            guess = Char.ToLower(guess);
            var correctLetterSet = ConvertStringToLetterSet(_correctWord);
            if (correctLetterSet.Contains(guess))
            {
                _correctGuessedLetters.Add(guess);
            }
            else
            {
                _incorrectGuessedLetters.Add(guess);
            }
        }

        public void GuessWholeWord(string wordGuess)
        {

        }
    }
}