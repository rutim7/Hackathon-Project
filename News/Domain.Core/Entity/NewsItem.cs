using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Enums;

namespace Domain.Core.Entity
{
  public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public int OrganisationId { get; set; }
        public Category Category { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual ICollection<ImageNews> Images { get; set; }
        [NotMapped]
        public int index { get; set; }
    }
}
