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
            result.gamestatus.ShouldBeEquivalentTo("Won");
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
            result.gamestatus.ShouldBeEquivalentTo("Lost");
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
    }

}