using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{
    public class SentimeContext : IdentityDbContext<ApplicationUser>
    {
        
    }
}
