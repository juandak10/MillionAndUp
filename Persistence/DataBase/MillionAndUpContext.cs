using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBase
{
    public class MillionAndUpContext : DbContext
    {
        public MillionAndUpContext(DbContextOptions<MillionAndUpContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>().HasKey(table => new {
                table.Code,
                table.MessageType
            });

        }

        public virtual DbSet<Account>? Account { get; set; }
        public virtual DbSet<Message>? Message { get; set; }

    }
}
