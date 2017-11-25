using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Core.Entity;
using News.Models;

namespace News.Controllers
{
    public class OrganisationController : Controller
    {
        // GET: Organisation
        public ActionResult Index()
        {
            return View();
        }

        private IServiceManager manager;
        public OrganisationController(IServiceManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public ActionResult Create()
        {
          
            return View("Create");
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Create(OrganisationViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    if (model.Image.FileName == String.Empty)
            //    {
            //        return new HttpStatusCodeResult(400, "Failed to upload image");
            //    }
            //    else if (!model.Image.ContentType.Contains("data:image"))
            //    {

                   
            //    }
            return View("Create");
        }
    }
}