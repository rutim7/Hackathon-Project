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
            _id = 0;
            var model = AllCategories.Where(x => x.Id == id).Select(sub => sub.SubCategories).FirstOrDefault();
            return PartialView("_SubCategories", model);
        }

        public ActionResult CreateNews ()
        {

            return View("CreateNews");
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