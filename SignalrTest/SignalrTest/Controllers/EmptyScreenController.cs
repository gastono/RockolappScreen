using SignalrTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalrTest.Controllers
{
    public class EmptyScreenController : Controller
    {
        RocolappDataAccess dataAccess = new RocolappDataAccess();

        // GET: EmptyScreen
        public ActionResult Index()
        {
            bool exists = false;

            int screenNumber;

            do
            {
                Random randomScreenNumberGenerator = new Random();

                screenNumber = randomScreenNumberGenerator.Next(999999);

                exists = dataAccess.CheckIfScreenExists(screenNumber);

            } while (exists);


            ViewData["ScreenNumber"] = screenNumber;

            return View();
        }
       
    }
}
