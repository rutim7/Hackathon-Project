﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entity;

namespace Domain.Core.Configurations
{
  public  class ImageNewsConfiguration : EntityTypeConfiguration<ImageNews>
    {
      public ImageNewsConfiguration()
      {
          ToTable("Images");
      }
    }
}
