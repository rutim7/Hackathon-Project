using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Core.Entity;

namespace Data.Impl
{
  public class CategoriesRepository: GenericRepository<Categories>
    {
        public CategoriesRepository(SentimeContext context) : base(context)
        {
        }
    }
}
