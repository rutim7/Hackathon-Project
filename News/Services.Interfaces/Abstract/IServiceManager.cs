using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Data
{
    public interface IServiceManager
    {
        IAppUserService AppUserService { get; }
        IOrganisationService OrganisationService { get; }
        INewsService NewsService { get; }
        ICategories CategoriesService { get; }
        SentimeContext DbContext { get; }
    }
}
