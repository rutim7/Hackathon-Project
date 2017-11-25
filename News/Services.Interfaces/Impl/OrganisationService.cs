using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Interfaces;

namespace Data.Services
{
   public class OrganisationService : OrganisationRepository,IOrganisationService
    {
        public OrganisationService(SentimeContext context) : base(context)
        {
        }

        public void Dispose()
        {
         
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
