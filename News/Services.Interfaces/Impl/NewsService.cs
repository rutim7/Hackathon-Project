using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Impl;
using Domain.Core.Entity;
using Domain.Interfaces;

namespace Services.Interfaces.Impl
{
    public class NewsService : NewsRepository, INewsService
    {
        public NewsService(SentimeContext context) : base(context)
        {
        }

        public void Dispose()
        {
        }

        public IEnumerable<NewsItem> GetNewsByCategory(List<string> filterCategiries)
        {
            List<NewsItem> result = new List<NewsItem>();

            foreach (var item in filterCategiries)
            {
                result.AddRange(DbSet.Where(n => n.Category.ToString() == item).OrderByDescending(n => n.DateCreated));
            }

            return result;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
