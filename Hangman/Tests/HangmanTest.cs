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
        [Test]
        public void TestGuessRight()
        {
            // assemble
            GameFacade.CreateGame();

            GameStatus gameStatus = GameFacade.GetGameStatus();

            // act
            GameFacade.Guess(gameStatus.CorrectWord);
            
            // asssert
            GameResult result = GameFacade.GetGameResult();
            result.GameState.ShouldBeEquivalentTo(GameState.Won);
        }

        [Test]
        public void TestGuessWrong()
        {
            // assemble
            GameFacade.CreateGame();

            GameStatus gameStatus = GameFacade.GetGameStatus();

            // act
            GameFacade.Guess(gameStatus.CorrectWord + "A");

            // asssert
            GameResult result = GameFacade.GetGameResult();
            result.GameState.ShouldBeEquivalentTo(GameState.Lost);
        }

        [Test]
        public void TestGuessNull()
        {
            // assemble
            GameFacade.CreateGame();
            
            // act
            GameFacade.Guess(null);

            //assert
            GameResult result = GameFacade.GetGameResult();
            result.GameState.ShouldBeEquivalentTo(GameState.Lost);
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
        public void TestSingleLetterWordGeneratorWordGeneration()
        {
            string word = WordGenerator.GenerateWord(1);

            Assert.That(word.Length, Is.GreaterThan(0));
            Assert.That(word.Length, Is.LessThan(2));
        }

        [Test]
        public void TestResumeActiveGame()
        {
            GameFacade.CreateGame();
            GameStatus gameStatus = GameFacade.GetGameStatus();

            //Creating a new game should just return the existing game.
            GameFacade.CreateGame();
            GameStatus gameStatus2 = GameFacade.GetGameStatus();

            gameStatus.CorrectWord.ShouldBeEquivalentTo(gameStatus2.CorrectWord);
        }

        [Test]
        public void TestGameOverNewGame()
        {
            GameFacade.CreateGame();
            GameStatus gameStatus = GameFacade.GetGameStatus();
            GameFacade.Guess("A");
            GameFacade.EndGame();

            GameFacade.CreateGame();
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
            GameFacade.Guess("A");
            GameFacade.EndGame();
            //assert
            GameFacade.IsGameInProgress.Should().BeFalse();
        }
    }

}