using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Events;
using Events.IO.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Events.IO.Infra.Data.Mappings
{
    public class AddressMapping : EntityTypeConfiguration<Address>
    {
        public override void Map(EntityTypeBuilder<Address> builder)
        {
            builder.Property(e => e.Address1)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Address2)
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnType("varchar(6)");

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Province)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            //Organizer foreign key
            builder.HasOne(e => e.Event)
                .WithOne(e => e.Address)
                .HasForeignKey<Address>(e => e.EventId)
                .IsRequired(false);

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Addresses");
        }
    }
}
