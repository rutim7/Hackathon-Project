﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
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
        public  ActionResult GetAllNewsInOrganisation(int id)
        {
            var organisation = manager.OrganisationService.FindSync(id);
            return PartialView("_OrganisationNews", organisation.News.ToList());
        }

        public ActionResult AddNewTemplate(NewsItem model)
        {
            return View("_NewsItem",model);
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

            ViewBag.HasCategory = !string.IsNullOrEmpty(CurrentUser.UserCategories);
            if (ViewBag.HasCategory)
            {
                List<string> filterCategiries = CategoryHelper.GetUserCategory(CurrentUser.UserCategories);
                news = manager.NewsService.GetNewsByCategory(filterCategiries);
            }
            else
            {
                news = await manager.NewsService.GetAll();
            }

            return View(news.ToList());
        }

        [Authorize]
        [HttpPost]
        public RedirectToRouteResult CreateNews(string text, string title, HttpPostedFileBase image, int orgId, Category category)
        {
            var picture = System.Web.HttpContext.Current.Request.Files["image"];
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
        [HttpPost]
        public JsonResult SaveCategories(string categories)
        {
            CurrentUser.UserCategories = categories;
           manager.AppUserService.UpdateEntity(CurrentUser);
            manager.AppUserService.SaveChanges();
            return Json(new {status="OK"});
        }

        public ActionResult GetUserCategories()
        {
            List<string> model = new List<string>();
            if (!string.IsNullOrEmpty(CurrentUser?.UserCategories))
            {
                model = CategoryHelper.GetUserCategory(CurrentUser.UserCategories);
            }

            return PartialView("_SubCategories", model);
        }
    }
}