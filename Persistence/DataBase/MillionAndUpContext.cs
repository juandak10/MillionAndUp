using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBase
{
    public class MillionAndUpContext : DbContext
    {
        public MillionAndUpContext(DbContextOptions<MillionAndUpContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder){}

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }

    }
}
