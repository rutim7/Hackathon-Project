using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Configurations;
using Domain.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{
    public class SentimeContext : IdentityDbContext<ApplicationUser>
    {
        //#region DbSet
        public virtual DbSet<NewsItem> NewsItems { get; set; }
        //#endregion DbSet
        public SentimeContext():base("DefaultConnection")
        {
           
        }

        public static SentimeContext Create()
        {
            return new SentimeContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new OrganisationConfiguration());
            modelBuilder.Configurations.Add(new NewsConfiguration());
            modelBuilder.Configurations.Add(new ImageNewsConfiguration());
            modelBuilder.Entity<Organisation>().HasMany<ApplicationUser>(o => o.Subscribers).WithMany(u => u.Organisations)
               .Map(m =>
               {
                   m.MapLeftKey("OrganisationId");
                   m.MapRightKey("SubscriberId");
                   m.ToTable("Subscribers");
               });
            base.OnModelCreating(modelBuilder);
        }

    }
}
