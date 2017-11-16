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
            GameFacade.CreateGame();
            return View();
        }

        public ActionResult Guess(string value)
        {
            GameFacade.Guess(value);

            return View(GameFacade.GetGameResult());
        }


    }
}