using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Hangman.Models;

namespace Hangman.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = "JP";
            else
                Session["UserId"] = "JPSSS";
            ViewBag.SessionId = Session["UserId"];
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

        public ActionResult Guess(char value)
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

    }
}