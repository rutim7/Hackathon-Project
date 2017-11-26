using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entity
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OwnerId { get; set; }
        public int Rating { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<ApplicationUser> Subscribers { get; set; }
        //public virtual int OrganisationId { get; set; }
        public string Avatar { get; set; }

        public virtual ICollection<NewsItem> News { get; set; }
    }
}
