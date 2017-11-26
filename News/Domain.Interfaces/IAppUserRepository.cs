using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Entity;

namespace Domain.Interfaces
{
    public interface IAppUserService : IDisposable, IGeneralService<ApplicationUser>
    {
        ApplicationUser GetByName(string id);
    }
}
