using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Data;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using News.Models;
using ApplicationUser = Domain.Core.Entity.ApplicationUser;

namespace News.Controllers.abstr
{
    public abstract class GenerallController : Controller
    {
        // GET: General
        protected UserManager<ApplicationUser> UserManager { get; set; }
        

        //public ApplicationUser CurrentUser { get; private set; }
        private IIdentity _identity;
        private IServiceManager _manager;
        private ApplicationUser _currentUser;
        private HttpContextBase _context;
        public ApplicationUser CurrentUser { get; private set; }
        //public ApplicationUser CurrentUser
        //{
        //    get
        //    {
        //        if (_currentUser == null)
        //        {
        //            var _identity = _context.User.Identity;
        //            _currentUser = _manager.AppUserService.FindNoAsync(_identity.Name);
        //        }

        //        return _currentUser;
        //    }
        //    set { _currentUser = value; }
        //}
        protected GenerallController(IServiceManager manager)
        {
            _manager = manager;
        }

        protected override  void Initialize(RequestContext rc)
        {
            _identity = rc.HttpContext.User.Identity;

            if (_identity.Name != null && !"".Equals(_identity.Name))
            {
                CurrentUser =  _manager.AppUserService.GetByName(_identity.Name);
            }
            base.Initialize(rc);

        }
    }
}