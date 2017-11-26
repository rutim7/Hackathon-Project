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
using News.Controllers.abstr;
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
        public NewsController(IServiceManager manager):base(manager)
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
        public ActionResult Index()
        {
           List<NewsItem> news = new List<NewsItem>()
           {
               new NewsItem {Title ="Як робити замовлення на AliExpress.10 ключових моментів",
                   Text = "Як почати купувати  і  бути щасливим",index = 1},
               new NewsItem {Title ="Запрошуєм всіх на Хакатон",Text = "Що таке Хакатон та історії про Хакатоняшок",index = 2},
               new NewsItem {Title ="Дівчата з Хакатону",Text = "Найкращі дівчата які відвідали Хакатон",index = 3},
               new NewsItem
               {
                   Title ="Пожена в центральній частині міста",Text = "Найдзвичайна ситуація відбулася в центральній частині міста. Троє постраждалих",
                   index = 4
               },
               new NewsItem {Title ="Страшилки у  Івано-Франківську",Text = "Історія про Чорну Марію, про Бабая ",index = 5},
               new NewsItem {Title ="Знову у Франківську",Text = "На околицях Франківська, у Вовчинецьках викрали курей.",index = 6},
               new NewsItem {Title ="Користь та шкода алкоголю",Text = "Наша редакція дослідити це питання власноруч та готова" +
                                                                       "поділитися з вами своїми висновками",index = 7},
               new NewsItem {Title ="Рецензія на Лігу Справедливості",Text = "Згадуєм хто такий Бетмен, Чудо-Жінка та знайомимось з новими персонажами",index = 8}

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
   }
}