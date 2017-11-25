using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entity;

namespace Domain.Core.Configurations
{
    public class OrganisationConfiguration : EntityTypeConfiguration<Organisation>
    {
        public OrganisationConfiguration()
        {
            ToTable("Organisations");

            HasKey(o => o.Id);

            Property(c => c.Name).IsRequired();

            //HasRequired(o => o.Owner)
            //    .WithMany(o => o.Organisations)
            //    .HasForeignKey(o => o.OwnerId)
            //    .WillCascadeOnDelete(true);



            //Property(c => c.Address).IsRequired();
        }
    }
}
