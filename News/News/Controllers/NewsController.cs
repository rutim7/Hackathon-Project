using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Core;
using Data;

namespace SenTime.Controllers
{
    public class NewsController : Controller
    {
        private IServiceManager manager;
        public NewsController(IServiceManager manager)
        {
            this.manager = manager;
        }
        // GET: Newa
        public ActionResult Index()
        {
           List<Domain.Core.News> news = new List<Domain.Core.News>()
           {
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggggggg"}
           };
            return View(news);
        }
     
    }
}