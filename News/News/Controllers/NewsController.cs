using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain.Core.Entity;
using Data;
using News.Controllers.abstr;
using News.Helpers;
using News.Models;

namespace News.Controllers
{
    public class NewsController : GenerallController
    {
        public static int _id = 0;

        public IList<Categories> AllCategories = new List<Categories>
        {
            new Categories ("Спорт", new [] {"Футбол","Баскетбол", "Бокс"} ) ,
            new Categories("Техніка", new [] {"Інновації","Нанотехнології", "Apple"}),
            new Categories("Політика", new [] {"Закони","Євробляхи", "Корупція"}),
            new Categories("Економіка", new [] {"НацБанк","МВФ", "Курс"}),
            new Categories("Розваги", new [] {"Фільми","Ігри", "уваі"}),
            new Categories("Відпочинок", new [] {"Популярні курорти", "Тури", "Корупція"})
        };
        public class Categories
        {
            public string Name;
            public int Id;
            public IList<SubCategories> SubCategories = new List<SubCategories>();

            public Categories(string _name, string[] subCategories)
            {
                Name = _name;
                Id = ++_id;
                foreach (var subCategoryName in subCategories)
                {
                    var subCategory = new SubCategories(subCategoryName, Id);
                    SubCategories.Add(subCategory);
                }
            }
        }

        public class SubCategories
        {
            public string Name;
            public int ParentId;
            public bool isChecked;
            public SubCategories(string _name, int _parentId)
            {
                Name = _name;
                ParentId = _parentId;
                isChecked = false;
            }
        }

        private IServiceManager manager;
        public NewsController(IServiceManager manager):base(manager)
        {
            this.manager = manager;
        }

        public ActionResult AddNewTemplate(NewsItem model)
        {
            return View("_NewsItem");
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
            _id = 0;
            var model = AllCategories.Where(x => x.Id == id).Select(sub => sub.SubCategories).FirstOrDefault();
            return PartialView("_SubCategories", model);
        }

        public ActionResult CreateNews ()
        {

            return View("_CreateNews");
        }
        [HttpPost]
        public ActionResult CreateResult(NewsItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image.FileName == String.Empty)
                {
                    return new HttpStatusCodeResult(400, "Failed to upload image");
                }
                else if (!model.Image.ContentType.Contains("data:image"))
                {
                    var blobContainer = new SenTimeBlobContainer();
                    NewsItem org;
                    using (var memoryStream = new MemoryStream())
                    {
                        model.Image.InputStream.CopyTo(memoryStream);
                        blobContainer.SaveFile(model.Image.FileName, model.Image.ContentType, memoryStream.ToArray());
                        //model.Avatar = model.Image.FileName;
                        org = new NewsItem()
                        {
                            Title = model.Title,
                            Text = model.Text,
                           Images = model.Images
                        
                        };
                        org.Images.Add(new ImageNews()
                        {
                            ImageThumbnail = model.Image.FileName
                        });
                        manager.NewsService.Add(org);
                        manager.NewsService.SaveChanges();
                    }
                    return Json( new { data = org });

                }

            }
            return null;

        }



        //[Authorize]
        //[HttpPost]
        //public ActionResult CreateNews(NewsItem model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Image.FileName == String.Empty)
        //        {
        //            return new HttpStatusCodeResult(400, "Failed to upload image");
        //        }
        //        else if (!model.Image.ContentType.Contains("data:image"))
        //        {
        //            var blobContainer = new SenTimeBlobContainer();
        //            Organisation org;
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                model.Image.InputStream.CopyTo(memoryStream);
        //                blobContainer.SaveFile(model.Image.FileName, model.Image.ContentType, memoryStream.ToArray());
        //                model.Avatar = model.Image.FileName;
        //                org = new Organisation()
        //                {
        //                    Name = model.Name,
        //                    Avatar = model.Image.FileName
        //                };
        //                manager.OrganisationService.Add(org);
        //                manager.OrganisationService.SaveChanges();
        //            }
        //            return RedirectToAction("Datails", new { id = org.Id });

        //        }

        //    }
        //    return null;
        //}

        [HttpPost]
        public JsonResult SetNewSubCategories(int id)
        {
            return Json(new {status="OK"});
                
        }
   }
}