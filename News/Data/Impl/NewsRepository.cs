using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Core.Entity;

namespace Data.Impl
{
    public class NewsRepository : GenericRepository<NewsItem>
    {
        public NewsRepository(SentimeContext context) : base(context)
        {
        }
    }
}
