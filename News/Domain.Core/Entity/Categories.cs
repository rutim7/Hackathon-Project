using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entity
{
  public  class Categories
    {
        public string UserId { get; set; }
        public bool Sport { get; set; }
        public bool Technology { get; set; }
        public bool Politic { get; set; }
        public bool Rest { get; set; }
        public bool Society { get; set; }
        public bool Auto { get; set; }
        public bool Medicine { get; set; }
    }
}
