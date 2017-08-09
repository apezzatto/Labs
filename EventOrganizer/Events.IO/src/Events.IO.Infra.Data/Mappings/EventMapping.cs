using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Events;
using Events.IO.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.IO.Infra.Data.Mappings
{
    public class EventMapping : EntityTypeConfiguration<Event>
    {
        public override void Map(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.ShortDescription)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.LongDescription)
                .HasColumnType("varchar(max)");

            builder.Property(e => e.CompanyName)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.Tags);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Events");

            //Organizer foreign key
            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerId);

            //Category foreign key
            builder.HasOne(e => e.Category)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired(false);
        }
    }
}
