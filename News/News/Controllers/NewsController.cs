using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain.Core.Entity;
using Data;
using Domain.Core.Enums;
using News.Controllers.abstr;
using News.Helpers;
using News.Models;

namespace News.Controllers
{
    public class NewsController : GenerallController
    {
        private IServiceManager manager;
        public NewsController(IServiceManager manager):base(manager)
        {
            this.manager = manager;
        }

        // GET: News
        public ActionResult Index()
        {
           List<NewsItem> news = new List<NewsItem>()
           {
               new NewsItem {Title ="ggggg",Text = "Ggggggffffffffffffffffffffffffffffffgggggggggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Gggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Ggggggggggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Gggggggggggggggggggggggfewefvrevrevrvvrgggg"},
               new NewsItem {Title ="ggggg",Text = "Ggggggsdcvdrgdgdgdgggggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Gggggggggggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Gggggggggggggggggggggggggggg"},
               new NewsItem {Title ="ggggg",Text = "Gggggggggggggggggggggggggggg"}

           };
            return View(news);
        }
        [HttpPost]
        public ActionResult GetSubCategories(int id)
        {
            //_id = 0;
            //var model = AllCategories.Where(x => x.Id == id).Select(sub => sub.SubCategories).FirstOrDefault();
            return PartialView("_SubCategories");
        }

        [Authorize]
        [HttpPost]
        public RedirectToRouteResult CreateNews(string text, string title, HttpPostedFileBase image, int orgId, Category category)
        { 
            if (image.FileName == String.Empty)
            {
                return null;

            }
                else if (!image.ContentType.Contains("data:image"))
                {
                    var blobContainer = new SenTimeBlobContainer();
                    NewsItem newsItem;
                    using (var memoryStream = new MemoryStream())
                    {
                        image.InputStream.CopyTo(memoryStream);
                        blobContainer.SaveFile(image.FileName, image.ContentType, memoryStream.ToArray());
                        newsItem = new NewsItem()
                        {
                            Title = title,
                            Text = text,
                            AvatarNews = image.FileName,
                            OrganisationId = orgId,
                            DateCreated = DateTime.Now,
                            Category = category
                        };
                        manager.NewsService.Add(newsItem);
                        manager.NewsService.SaveChanges();
                    }
                    return RedirectToAction("Details","Organisation",new {id=orgId});

                }
            return null;
        }

        public JsonResult SaveCategories(string category)
        {
            return Json(new { });
        }
   }
}