using System.IO;
using Events.IO.Domain.Events;
using Events.IO.Domain.Organizers;
using Events.IO.Infra.Data.Extensions;
using Events.IO.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Events.IO.Infra.Data.Context
{
    public class ContextEvents : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new EventMapping());
            modelBuilder.AddConfiguration(new AddressMapping());
            modelBuilder.AddConfiguration(new OrganizerMapping());
            modelBuilder.AddConfiguration(new CategoryMapping());

            base.OnModelCreating(modelBuilder); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
