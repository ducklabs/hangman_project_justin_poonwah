using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangman.Models;

namespace Hangman.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartGame()
        {
            GameFacade.CreateGame();
            return RedirectToAction("ShowGame");
        }

        public ActionResult ShowGame()
        {
            if (!GameFacade.IsGameInProgress)
                return RedirectToAction("Index");

            ViewBag.ImagePath = ImagePath;
            return View(GameFacade.GetGameStatus());
        }

        public ActionResult EndGame()
        {
            if (!GameFacade.IsGameInProgress)
                return RedirectToAction("Index");

            if(!GameFacade.IsGameOver)
                return RedirectToAction("ShowGame");

            var gameResult = GameFacade.GetGameResult();
            GameFacade.EndGame();
            return View(gameResult);
        }

        public ActionResult Guess(string value)
        {
            GameFacade.Guess(value);

            if(GameFacade.IsGameOver)
                return RedirectToAction("EndGame");
            return RedirectToAction("ShowGame");
        }

        public ActionResult Reset()
        {
            return Redirect("Index");
        }

        public string ImagePath
        {
            get { return "~/Content/Images/hang" + GameFacade.GetGameStatus().IncorrectGuessedLetters.Length + ".gif"; }
        }

    }
}