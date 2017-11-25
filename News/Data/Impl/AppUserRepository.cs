using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Core;

namespace Data.Impl
{
    class AppUserRepository:GenericRepository<Organisation>
    {
        public AppUserRepository(SentimeContext context) : base(context)
        {
        }
    }
}
