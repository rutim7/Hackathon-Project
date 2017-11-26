using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain.Core.Entity;
using Data;
using Domain.Core.Enums;
using News.Controllers.Abstract;
using News.Helpers;
using News.Models;

namespace News.Controllers
{
    public class NewsController : GenerallController
    {
        private IServiceManager manager;

        public NewsController(IServiceManager manager) : base(manager)
        {
            this.manager = manager;
        }

        // GET: News
        public async Task<ActionResult> Index()
        {
            IEnumerable<NewsItem> news;

            if (CurrentUser == null)
            {
                news = await manager.NewsService.GetAll();
                return View(news.ToList());
            }

            List<string> filterCategiries = CategoryHelper.GetUserCategory(CurrentUser.UserCategories);
            news = manager.NewsService.GetNewsByCategory(filterCategiries);

            return View(news.ToList());
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