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
               new Domain.Core.News {Title ="ggggg",Content = "Ggggggffffffffffffffffffffffffffffffgggggggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/1.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggg",ImagePath ="~/Content/NewsImages/2.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Ggggggggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/3.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggfewefvrevrevrvvrgggg",ImagePath ="~/Content/NewsImages/4.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Ggggggsdcvdrgdgdgdgggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/2.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/1.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/3.png"},
               new Domain.Core.News {Title ="ggggg",Content = "Gggggggggggggggggggggggggggg",ImagePath ="~/Content/NewsImages/4.png"}

           };
            return View(news);
        }
     
    }
}