using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Entity;

namespace Domain.Interfaces
{
    public interface IOrganisationService : IDisposable, IGeneralService<Organisation>
    {
        List<Organisation> GetOrgByUSer(string id);


        void Save();
    }
}
