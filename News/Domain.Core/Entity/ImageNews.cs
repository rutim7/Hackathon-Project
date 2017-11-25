using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entity
{
   public class ImageNews
    {
        public int Id { get; set; }
        public string ImageThumbnail { get; set; }
        public int NewsId { get; set; }
        public virtual NewsItem News { get; set; }
    }
}
