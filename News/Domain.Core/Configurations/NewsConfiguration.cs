using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entity;

namespace Domain.Core.Configurations
{
    public class NewsConfiguration : EntityTypeConfiguration<NewsItem>
    {
        public NewsConfiguration()
        {
            ToTable("News");
            HasKey(n => n.Id);

            Property(n => n.Title).IsRequired();

            Property(n => n.Text).IsRequired();

            //HasRequired(n => n.Organisation);

            //HasMany(c => c.Images).WithOptional(p => p.News).HasForeignKey(k => k.NewsId);
            HasRequired(n => n.Organisation).WithMany(c => c.News).HasForeignKey(j => j.OrganisationId);
            //HasMany(c => c.News).WithOptional(n => n.Organisation).HasForeignKey(k => k.OrganisationId);
        }
    }
}
