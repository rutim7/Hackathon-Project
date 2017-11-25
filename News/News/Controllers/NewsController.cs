using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Domain.Core;
using Data;

namespace News.Controllers
{
    public class NewsController : Controller
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
        public NewsController(IServiceManager manager)
        {
            this.manager = manager;
        }

        // GET: News
        public ActionResult Index()
        {
            //_id = 0;
            //return View(AllCategories);
            List<Domain.Core.Entity.News> news = new List<Domain.Core.Entity.News>()
            {
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"},
                new Domain.Core.Entity.News {Title ="ggggg",Text = "Gggggggggggggggggggggggggggggggg"}
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

        [HttpPost]
        public JsonResult SetNewSubCategories(int id)
        {
            return Json(new {status="OK"});
                
        }
   }
}