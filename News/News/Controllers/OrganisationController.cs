using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Core.Entity;
using News.Controllers.Abstract;
using News.Helpers;
using News.Models;

namespace News.Controllers
{
    public class OrganisationController : GenerallController
    {
        // GET: Organisation
        public async Task<ActionResult> Index()
        {     
            //return View(manager.OrganisationService.Find(1));
            var listOrganisations = await manager.OrganisationService.GetAll();
            return View("ListOrganisations", listOrganisations);
        }

        private IServiceManager manager;

        public OrganisationController(IServiceManager manager):base(manager)
        {
            this.manager = manager;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }

        public async Task<ActionResult> Details(int id)
        {
            var organisation = await manager.OrganisationService.Find(id);
            ViewBag.IsOwner = CurrentUser.Id == organisation.OwnerId;

            return View("Details", organisation);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(OrganisationViewModel model)
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
                    Organisation org;
                    using (var memoryStream = new MemoryStream())
                    {
                        model.Image.InputStream.CopyTo(memoryStream);
                        blobContainer.SaveFile(model.Image.FileName, model.Image.ContentType, memoryStream.ToArray());
                        model.Avatar = model.Image.FileName;
                         org= new Organisation()
                        {
                            Name = model.Name,
                            Avatar = model.Image.FileName,
                            OwnerId = CurrentUser.Id 
                       };
                        manager.OrganisationService.Add(org);
                        manager.OrganisationService.SaveChanges();
                    }
                    return RedirectToAction("Details", new { id = org.Id});

                }
               
            }
            return null;
        }
    }
}