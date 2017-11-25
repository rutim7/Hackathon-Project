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
    public class NewsService : NewsRepository, INewsService
    {
        public NewsService(SentimeContext context) : base(context)
        {
        }

        public void Dispose()
        {

        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
