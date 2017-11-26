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
        //public static int _id = 0;

        //public IList<Categories> AllCategories = new List<Categories>
        //{
        //    new Categories ("Спорт", new [] {"Футбол","Баскетбол", "Бокс"} ) ,
        //    new Categories("Техніка", new [] {"Інновації","Нанотехнології", "Apple"}),
        //    new Categories("Політика", new [] {"Закони","Євробляхи", "Корупція"}),
        //    new Categories("Економіка", new [] {"НацБанк","МВФ", "Курс"}),
        //    new Categories("Розваги", new [] {"Фільми","Ігри", "уваі"}),
        //    new Categories("Відпочинок", new [] {"Популярні курорти", "Тури", "Корупція"})
        //};
        //public class Categories
        //{
        //    public string Name;
        //    public int Id;
        //    public IList<SubCategories> SubCategories = new List<SubCategories>();

        //    public Categories(string _name, string[] subCategories)
        //    {
        //        Name = _name;
        //        Id = ++_id;
        //        foreach (var subCategoryName in subCategories)
        //        {
        //            var subCategory = new SubCategories(subCategoryName, Id);
        //            SubCategories.Add(subCategory);
        //        }
        //    }
        //}

        //public class SubCategories
        //{
        //    public string Name;
        //    public int ParentId;
        //    public bool isChecked;
        //    public SubCategories(string _name, int _parentId)
        //    {
        //        Name = _name;
        //        ParentId = _parentId;
        //        isChecked = false;
        //    }
        //}

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
   }
}