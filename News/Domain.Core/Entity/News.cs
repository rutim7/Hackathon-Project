﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entity
{
  public  class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public int OrganisationId { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual ICollection<ImageNews> Images { get; set; }
    }
}
