using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Events.IO.Infra.Data.Context;

namespace Events.IO.Infra.Data.Migrations
{
    [DbContext(typeof(ContextEvents))]
    [Migration("20170809025937_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Events.IO.Domain.Events.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Address2")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("EventId");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Events.IO.Domain.Events.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Events.IO.Domain.Events.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid?>("CategoryId");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("Excluded");

                    b.Property<bool>("IsFree");

                    b.Property<string>("LongDescription")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("Online");

                    b.Property<Guid?>("OrganizerId");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Events.IO.Domain.Organizers.Organizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("SIN")
                        .IsRequired()
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("Events.IO.Domain.Events.Address", b =>
                {
                    b.HasOne("Events.IO.Domain.Events.Event", "Event")
                        .WithOne("Address")
                        .HasForeignKey("Events.IO.Domain.Events.Address", "EventId");
                });

            modelBuilder.Entity("Events.IO.Domain.Events.Event", b =>
                {
                    b.HasOne("Events.IO.Domain.Events.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Events.IO.Domain.Organizers.Organizer", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId");
                });
        }
    }
}
