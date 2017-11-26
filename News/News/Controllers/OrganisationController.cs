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

        public ActionResult GetMyOrg()
        {

            return PartialView("_MyOrg", manager.OrganisationService.GetOrgByUSer(CurrentUser.Id));
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

        [HttpPost]
        public ActionResult SaveProfilePicture(string imageContent,int id)
        {
            if (imageContent == String.Empty)
            {
                return new HttpStatusCodeResult(400, "Failed to upload image");
            }
            else if (!imageContent.Contains("data:image"))
            {
                const int widthLarge = 675;
                const int heightLarge = 450;

                var blobContainer = new SenTimeBlobContainer();
                Organisation organisation = manager.OrganisationService.FindSync(id);
                organisation.Avatar = blobContainer.GetPictureWithDefinedSizeAndExtention(imageContent, organisation.Avatar, widthLarge, heightLarge,
                    "Jpeg", "");
                manager.OrganisationService.UpdateEntity(organisation);
                manager.OrganisationService.SaveChanges();

            }
            return Json(new {js="sucess"});
        }


        public ActionResult ShowUploadImageContent()
        {
            return PartialView("_ImageUpload");
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
                    //return RedirectToAction("Details", new { id = org.Id });
                    return RedirectToAction("Index", "User");





                }

            }
            return null;
        }
    }
}