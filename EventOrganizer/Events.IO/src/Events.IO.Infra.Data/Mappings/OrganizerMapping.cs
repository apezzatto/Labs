using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Events;
using Events.IO.Domain.Organizers;
using Events.IO.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.IO.Infra.Data.Mappings
{
    public class OrganizerMapping : EntityTypeConfiguration<Organizer>
    {
        public override void Map(EntityTypeBuilder<Organizer> builder)
        {
            builder.Property(e => e.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.SIN)
                .HasColumnType("varchar(9)")
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Organizers");
        }
    }
}
