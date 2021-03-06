﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;

namespace Domain.Interfaces
{
    public interface IOrganisationService : IDisposable, IGeneralService<Organisation>
    {
        void Save();
    }
}
