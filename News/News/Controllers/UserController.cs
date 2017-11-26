using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using News.Controllers.abstr;

namespace News.Controllers
{
    [Authorize]
    public class UserController : GenerallController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public UserController(IServiceManager manager) : base(manager)
        {
        }
    }
}