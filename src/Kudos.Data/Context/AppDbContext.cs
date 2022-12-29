using Kudos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kudos.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Kudo> Kudo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Kudo>()
                .HasData(
                new Kudo
                {
                    Id = 1,
                    Reason = "Team Player",
                    Description = "text, text, text, text, text, text, text, text",
                    SenderId = 1,
                    ReceiverId = 2,
                },
                new Kudo
                {
                    Id = 2,
                    Reason = "Ownership Mindset",
                    Description = "text, text, text, text, text, text, text, text",
                    SenderId = 2,
                    ReceiverId = 1,
                });

            builder.Entity<Employee>()
              .HasData(
              new Employee
              {
                  Id = 1,
                  Name = "Name",
                  Surname = "Surname"
              },
              new Employee
              {
                  Id = 2,
                  Name = "Name",
                  Surname = "Surname"
              });
        }
    }
}