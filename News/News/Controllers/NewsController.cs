using System;
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

        public NewsController(IServiceManager manager) : base(manager)
        {
           
        }
        public  ActionResult GetAllNewsInOrganisation(int id)
        {
            var organisation = _manager.OrganisationService.FindSync(id);
            return PartialView("_OrganisationNews", organisation.News.ToList());
        }

        public ActionResult AddNewTemplate(NewsItem model)
        {
            return View("_NewsItem",model);
        }
        // GET: News
        public  ActionResult Index()
        {

            //IEnumerable<NewsItem> news;
            //if (CurrentUser == null)
            //{
            //    news = _manager.NewsService.GetAll();
            //    return View(news.ToList());
            //}

            //ViewBag.HasCategory = !string.IsNullOrEmpty(CurrentUser.UserCategories);
            //if (ViewBag.HasCategory)
            //{
            //    List<string> filterCategiries = CategoryHelper.GetUserCategory(CurrentUser.UserCategories);
            //    news = _manager.NewsService.GetNewsByCategory(filterCategiries);
            //}
            //else
            //{
            //    news = _manager.NewsService.GetAll();
            //}
            ViewBag.HasCategory = !string.IsNullOrEmpty(CurrentUser.UserCategories);
            List<NewsItem> news = new List<NewsItem>()
            {
                new NewsItem
                {
                    Title = "Як робити замовлення на AliExpress.10 ключових моментів",
                    Text = "Як почати купувати  і  бути щасливим",
                    index = 1
                },
                new NewsItem
                {
                    Title = "Запрошуєм всіх на Хакатон",
                    Text = "Що таке Хакатон та історії про Хакатоняшок",
                    index = 2
                },
                new NewsItem {Title = "Дівчата з Хакатону", Text = "Найкращі дівчата які відвідали Хакатон", index = 3},
                new NewsItem
                {
                    Title = "Пожена в центральній частині міста",
                    Text = "Найдзвичайна ситуація відбулася в центральній частині міста. Троє постраждалих",
                    index = 4
                },
                new NewsItem
                {
                    Title = "Страшилки у  Івано-Франківську",
                    Text = "Історія про Чорну Марію, про Бабая ",
                    index = 5
                },
                new NewsItem
                {
                    Title = "Знову у Франківську",
                    Text = "На околицях Франківська, у Вовчинецьках викрали курей.",
                    index = 6
                },
                new NewsItem
                {
                    Title = "Користь та шкода алкоголю",
                    Text = "Наша редакція дослідити це питання власноруч та готова" +
                           "поділитися з вами своїми висновками",
                    index = 7
                },
                new NewsItem
                {
                    Title = "Рецензія на Лігу Справедливості",
                    Text = "Згадуєм хто такий Бетмен, Чудо-Жінка та знайомимось з новими персонажами",
                    index = 8
                }
            };
            ViewBag.HardCode = true;
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
                  
                    using (var memoryStream = new MemoryStream())
                    {
                        image.InputStream.CopyTo(memoryStream);
                        blobContainer.SaveFile(image.FileName, image.ContentType, memoryStream.ToArray());
                        
                        
                        
                    }
                    NewsItem newsItem = new NewsItem()
                    {
                        Title = title,
                        Text = text,
                        AvatarNews = image.FileName,
                        OrganisationId = orgId,
                        DateCreated = DateTime.Now,
                        Category = category
                    };
                    _manager.NewsService.Add(newsItem);
                    _manager.NewsService.SaveChanges();
                return RedirectToAction("Details","Organisation",new {id=orgId});

                }
            return null;
        }
        [HttpPost]
        public JsonResult SaveCategories(string categories)
        {
            CurrentUser.UserCategories = categories;
            _manager.AppUserService.UpdateEntity(CurrentUser);
            _manager.AppUserService.SaveChanges();
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