using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentAssertions;
using FluentAssertions.Common;
using Hangman.Models;
using NUnit.Framework;

namespace Hangman.Tests
{
    [TestFixture]
    public class HangmanTest
    {
        [TearDown]
        public void TearDown()
        {
            GameFacade.EndGame();
            WordGeneratorFactory.Instance = new PreMadeListWordGenerator();
        }

        [Test]
        public void TestGuessNull()
        {
            // assemble
            GameFacade.CreateGame();
            
            // act
            GameFacade.Guess(' ');

            //assert
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.CorrectGuessedLetters.Should().BeEmpty();
            gameStatus.IncorrectGuessedLetters.Should().BeEmpty();
            gameStatus.StepsIntoTheGallows.Should().Be(0);
        }

        [Test]
        public void TestGuessCorrectLetter()
        {
            // assemble
            GameFacade.CreateGame();
            char letterGuess = GameFacade.GetGameStatus().CorrectWord[0];

            // act
            GameFacade.Guess(letterGuess);

            // assert
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.CorrectGuessedLetters.Should().Contain(Char.ToLower(letterGuess));
            gameStatus.IncorrectGuessedLetters.Should().BeEmpty();
            gameStatus.StepsIntoTheGallows.Should().Be(0);
        }

        [Test]
        public void TestGuessIncorrectLetter()
        {
            // assemble
            GameFacade.CreateGame();

            // act
            GameFacade.Guess('1');

            // assert
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.IncorrectGuessedLetters.Should().Contain('1');
            gameStatus.CorrectGuessedLetters.Should().BeEmpty();
            gameStatus.StepsIntoTheGallows.Should().Be(1);
        }

        [Test]
        public void TestWordGuessedWithLetters()
        {
            // assert
            GameFacade.CreateGame();
            
            // act
            for (int i = 0; i < GameFacade.GetGameStatus().CorrectWord.Length; i++)
            {
                char letterGuess = GameFacade.GetGameStatus().CorrectWord[i];
                GameFacade.Guess(letterGuess);
            }

            // assemble
            GameFacade.GetGameResult().GameResultState.Should().Be(GameResultState.Won);
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.IncorrectGuessedLetters.Should().BeEmpty();
            gameStatus.CorrectGuessedLetters.Should().Contain(GameFacade.GetGameStatus().CorrectWord.ToLower().ToCharArray());
            gameStatus.StepsIntoTheGallows.Should().Be(0);
        }
        
        [Test]
        public void TestWordGuessedBackwards()
        {
            // assert
            GameFacade.CreateGame();

            // act
            for (int i = GameFacade.GetGameStatus().CorrectWord.Length - 1; i >= 0 ; i--)
            {
                char letterGuess = GameFacade.GetGameStatus().CorrectWord[i];
                GameFacade.Guess(letterGuess);
            }

            // assemble
            GameResult gameResult = GameFacade.GetGameResult();
            gameResult.GameResultState.Should().Be(GameResultState.Won);
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.IncorrectGuessedLetters.Should().BeEmpty();
            gameStatus.CorrectGuessedLetters.Should().Contain(GameFacade.GetGameStatus().CorrectWord.ToLower().ToCharArray());
            gameStatus.StepsIntoTheGallows.Should().Be(0);
            gameStatus.CorrectWord.Should().Be(gameResult.CorrectWord);
        }

        [Test]
        public void TestTooManyGuesses()
        {
            // assemble
            GameFacade.CreateGame();

            // act
            for (int i = 0; i < 10; i++)
            {
                GameFacade.Guess(i.ToString()[0]);
            }

            // assert
            GameResult gameResult = GameFacade.GetGameResult();
            gameResult.GameResultState.ShouldBeEquivalentTo(GameResultState.Lost);
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.IncorrectGuessedLetters.Should().Contain('1');
            gameStatus.CorrectGuessedLetters.Should().BeEmpty();
            gameStatus.StepsIntoTheGallows.Should().Be(10);
            gameStatus.CorrectWord.Should().Be(gameResult.CorrectWord);
        }

        [Test]
        public void TestCreateGameDifferentWordEachTime()
        {
            // assemble
            GameFacade.CreateGame();
            GameStatus firstGameStatus = GameFacade.GetGameStatus();
            GameFacade.EndGame();
            // act
            GameFacade.CreateGame();
            // assert
            GameStatus secondGameStatus = GameFacade.GetGameStatus();
            firstGameStatus.CorrectWord.Should().NotBeSameAs(secondGameStatus.CorrectWord);
        }

        [Test]
        public void TestMultiLetterWordGeneratorWordGeneration()
        {
            //assemble
            //act
            string word = new PreMadeListWordGenerator().GenerateNextWord();

            //assert
            Assert.That(word.Length, Is.GreaterThan(0));
        }

        [Test]
        public void TestResumeActiveGame()
        {
            //assemble
            GameFacade.CreateGame();
            GameStatus gameStatus = GameFacade.GetGameStatus();

            //act
            GameFacade.CreateGame();
            GameStatus gameStatus2 = GameFacade.GetGameStatus();

            //assert
            gameStatus.CorrectWord.Should().Be(gameStatus2.CorrectWord);
        }

        [Test]
        public void TestGameOverNewGame()
        {
            // assemble
            GameFacade.CreateGame();
            GameStatus gameStatus = GameFacade.GetGameStatus();
            GameFacade.Guess('A');
            GameFacade.EndGame();

            // act
            GameFacade.CreateGame();

            // assemble
            GameStatus gameStatus2 = GameFacade.GetGameStatus();

            //Random selection can fail this test.
            gameStatus.CorrectWord.Should().NotBeSameAs(gameStatus2.CorrectWord);
        }

        [Test]
        public void TestGameOverNoNewGame()
        {
            // assemble
            GameFacade.CreateGame();
            // act
            GameFacade.Guess('A');
            GameFacade.EndGame();
            //assert
            GameFacade.IsGameInProgress.Should().BeFalse();
        }
        

        [Test]
        public void Test1LetterMatchWith1Guess()
        {
            // assert
            var configurableWordGenerator = new ConfigurableWordGenerator();
            configurableWordGenerator.Word = "ziplocbag";
            WordGeneratorFactory.Instance = configurableWordGenerator;

            GameFacade.CreateGame();

            // act
            GameFacade.Guess('z');

            // act
            char letterGuess = GameFacade.GetGameStatus().CorrectWord[0];
            GameFacade.Guess(letterGuess);
            
            // assemble
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.IncorrectGuessedLetters.Should().BeEmpty();
            gameStatus.CorrectGuessedLetters.Count.Should().Be(1);
            gameStatus.StepsIntoTheGallows.Should().Be(0);
        }


        [Test]
        public void Test3LetterMatchWith9Guesses()
        {
            // assert
            var configurableWordGenerator = new ConfigurableWordGenerator();
            configurableWordGenerator.Word = "ziplocbag";
            WordGeneratorFactory.Instance = configurableWordGenerator;

            GameFacade.CreateGame();

            // incorrect guesses
            GameFacade.Guess('d');
            GameFacade.Guess('e');
            GameFacade.Guess('f');
            GameFacade.Guess('h');
            GameFacade.Guess('j');
            GameFacade.Guess('k');

            // correct guesses
            GameFacade.Guess('z');
            GameFacade.Guess('i');

            // act
            GameFacade.Guess('p');

            // assemble
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.CorrectGuessedLetters.Count.Should().Be(3);
            gameStatus.StepsIntoTheGallows.Should().Be(6);
        }

        [Test]
        public void TestAllLetterMatchWith9IncorrectGuesses()
        {
            // assert
            var configurableWordGenerator = new ConfigurableWordGenerator();
            configurableWordGenerator.Word = "ziplocbag";
            WordGeneratorFactory.Instance = configurableWordGenerator;

            GameFacade.CreateGame();

            // incorrect guesses
            GameFacade.Guess('d');
            GameFacade.Guess('e');
            GameFacade.Guess('f');
            GameFacade.Guess('h');
            GameFacade.Guess('j');
            GameFacade.Guess('k');
            GameFacade.Guess('m');
            GameFacade.Guess('n');
            GameFacade.Guess('q');

            // correct guesses
            GameFacade.Guess('z');
            GameFacade.Guess('i');
            GameFacade.Guess('p');
            GameFacade.Guess('l');
            GameFacade.Guess('o');
            GameFacade.Guess('c');
            GameFacade.Guess('b');
            GameFacade.Guess('a');

            // act
            GameFacade.Guess('g');


            // assemble
            HashSet<char> set = new HashSet<char>(GameFacade.GetGameStatus().CorrectWord.ToLower().ToCharArray());
            GameFacade.GetGameResult().GameResultState.Should().Be(GameResultState.Won);
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.CorrectGuessedLetters.Count.Should().Be(set.Count);
            gameStatus.StepsIntoTheGallows.Should().Be(9);
        }

        [Test]
        public void TestWordWithOneCharacterRepeated()
        {
            // assert
            var configurableWordGenerator = new ConfigurableWordGenerator();
            configurableWordGenerator.Word = "aaaaaaaaaa";
            WordGeneratorFactory.Instance = configurableWordGenerator;
            GameFacade.CreateGame();

            // act
            GameFacade.Guess('a');

            // assemble
            GameFacade.GetGameResult().GameResultState.Should().Be(GameResultState.Won);
            GameStatus gameStatus = GameFacade.GetGameStatus();
            gameStatus.CorrectGuessedLetters.Count.Should().Be(1);
            gameStatus.StepsIntoTheGallows.Should().Be(0);
        }
    }

}