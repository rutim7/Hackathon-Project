using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Core.Entity;
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

        public List<Organisation> GetOrgByUSer(string id)
        {
            return DbSet.Where(c => c.Owner.Id == id).ToList();
        }
        //public List<NewsItem> News(Organisation organisation)
        //{
        //    return Context.Entry(organisation).Entity.News.ToList();
        //}

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
