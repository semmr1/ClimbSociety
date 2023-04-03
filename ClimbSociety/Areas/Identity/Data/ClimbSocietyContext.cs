using ClimbSociety.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClimbSociety.Models;
using System.Reflection.Emit;

namespace ClimbSociety.Data;

public class ClimbSocietyContext : IdentityDbContext<Areas.Identity.Data.Climber>
{
    public ClimbSocietyContext(DbContextOptions<ClimbSocietyContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Climber>().HasRequired(m => m.ClimbingLevel);
        for (int i=3; i<=9;i++)
        {
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "A" });
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "B" });
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "C" });
        }
        modelBuilder.Entity<Climber>()
            .HasMany(e => e.Matches);
    }
    public DbSet<ClimbingLevel> ClimbingLevels { get; set; } = default!;
    public DbSet<Match> Matches { get; set; } = default!;
    public DbSet<ChatMessage> ChatMessage { get; set; } = default!;
}
