using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Impl;
using Domain.Interfaces;

namespace Services.Interfaces.Impl
{
  public class CategoriesService : CategoriesRepository,ICategories
    {
        public CategoriesService(SentimeContext context) : base(context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool UserHasCategories(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
