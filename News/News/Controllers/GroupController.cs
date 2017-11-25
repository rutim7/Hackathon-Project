using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class GroupController : Controller
    {
        // GET: Organisation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View("CreateGroup");
        }

        [HttpPost]
        public ActionResult CreateGroup(string model)
        {
            return View("CreateGroup");
        }
    }
}