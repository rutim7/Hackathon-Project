using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using News.Controllers.Abstract;

namespace News.Controllers
{
    public class HomeController : GenerallController 
    {
        private IServiceManager manager;

        public HomeController(IServiceManager serviceManager) : base(serviceManager)
        {
            this.manager = manager;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}