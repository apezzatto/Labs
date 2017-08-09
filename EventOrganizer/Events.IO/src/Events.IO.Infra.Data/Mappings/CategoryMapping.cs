using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Events;
using Events.IO.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.IO.Infra.Data.Mappings
{
    class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> builder)
        {
            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Categories");
        }
    }
}
