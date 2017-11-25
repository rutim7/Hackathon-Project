using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Services;
using Domain.Interfaces;

namespace Data
{
   public class ServiceManager:IServiceManager
    {
        private SentimeContext _context;
        private IOrganisationService _organisationService;

        public SentimeContext DbContext => _context ?? (_context = new SentimeContext());

        public IOrganisationService OrganisationService => _organisationService ?? (_organisationService = new OrganisationService(DbContext));
    }
}
