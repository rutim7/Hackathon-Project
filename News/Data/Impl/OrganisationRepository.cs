using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Entity;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class OrganisationRepository : GenericRepository<Organisation>
    {

        public OrganisationRepository(SentimeContext context) : base(context)
        {
        }


        //    private bool disposed = false;

        //    public virtual void Dispose(bool disposing)
        //    {
        //        if (!this.disposed)
        //        {
        //            if (disposing)
        //            {
        //                Context.Dispose();
        //            }
        //        }
        //        this.disposed = true;
        //    }

        //    public void Dispose()
        //    {
        //        Dispose(true);
        //        GC.SuppressFinalize(this);

        //    }
        //}
    }
}