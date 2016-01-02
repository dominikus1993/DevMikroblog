using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevMikroblog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult All()
        {
            return PartialView();
        }

        public ActionResult Tag()
        {
            return PartialView();
        }
    }
}
