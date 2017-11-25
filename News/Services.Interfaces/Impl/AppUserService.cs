using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Impl;
using Data.Repositories;
using Domain.Interfaces;

namespace Services.Interfaces.Impl
{
    public class AppUserService : AppUserRepository, IAppUserService
    {
        public AppUserService(SentimeContext context) : base(context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
