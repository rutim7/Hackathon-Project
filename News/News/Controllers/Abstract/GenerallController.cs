using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using Data;
using Domain.Core.Entity;

namespace News.Controllers.Abstract
{
    public abstract class GenerallController : Controller
    {
        // GET: General
        //public ApplicationUser CurrentUser { get; private set; }
        private IIdentity _identity;
        private IServiceManager _manager;
        public ApplicationUser CurrentUser { get; private set; }
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