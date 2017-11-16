using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentAssertions;
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

            GameStatus gameStatus = GameFacade.GetGameStatus();

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
            // act
            GameFacade.CreateGame();
            // assert
            GameStatus secondGameStatus = GameFacade.GetGameStatus();
            firstGameStatus.CorrectWord.Should().NotBeSameAs(secondGameStatus.CorrectWord);
        }

        [Test]
        public void TestWordGeneratorWordGeneration()
        {
            string word = WordGenerator.GenerateWord(1);

            Assert.That(word.Length, Is.GreaterThan(0));
            Assert.That(word.Length, Is.LessThan(2));
        }

        [Test]
        public void TestResumeActiveGame()
        {
            
        }

        [Test]
        public void TestGameOverNewGame()
        {
            
        }

        [Test]
        public void TestGameOverNoNewGame()
        {
            // assemble
            //GameFacade.CreateGame();
            // act
            //GameFacade.Guess("A");
            //End game 
            // assert
            //Assert the game is null
            //firstGameStatus.CorrectWord.Should().NotBeSameAs(secondGameStatus.CorrectWord);
        }
    }

}