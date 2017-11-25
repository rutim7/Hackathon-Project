﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Core.Entity;
using News.Helpers;
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

        public async Task<ActionResult> Datails(int id)
        {
            var organisation = await manager.OrganisationService.Find(id);
            return View("Details", organisation);
        }

        //[Authorize]
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
                            Avatar = model.Image.FileName
                        };
                        manager.OrganisationService.Add(org);
                        manager.OrganisationService.SaveChanges();
                    }
                    return RedirectToAction("Datails", new { id = org.Id});

                }
               
            }
            return null;
        }
    }
}