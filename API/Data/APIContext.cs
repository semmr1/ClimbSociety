using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Model;

namespace API.Data
{
    public class APIContext : DbContext
    {
        public APIContext (DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().Property(m => m.Id).ValueGeneratedOnAdd();
            Message message = new()
            {
                Id = -1,
                Email = "test@test.nl",
                Subject = "Test",
                MessageText = "Dit is een test"
            };
            modelBuilder.Entity<Message>().HasData(message);
        }
    }
}
