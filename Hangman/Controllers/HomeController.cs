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
            return View(GameFacade.GetGameStatus());
        }

        public ActionResult EndGame()
        {
            var gameResult = GameFacade.GetGameResult();
            GameFacade.EndGame();
            return View(gameResult);
        }

        public ActionResult Guess(string value)
        {
            GameFacade.Guess(value);

            return RedirectToAction("EndGame");
            //return View();
        }

        public ActionResult Reset()
        {
            return Redirect("Index");
        }

    }
}