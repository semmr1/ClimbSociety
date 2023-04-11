using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClimbSociety.Models;
using System;

namespace ClimbSociety.Areas.Identity.Data;

public class ClimbSocietyContext : IdentityDbContext<Climber>
{
    public ClimbSocietyContext(DbContextOptions<ClimbSocietyContext> options)
        : base(options)
    {
    }

    public DbSet<Climber> Climbers { get; set; }
    public DbSet<ClimbingLevel> ClimbingLevels { get; set; }
    public DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Climber>().HasRequired(m => m.ClimbingLevel);
        for (int i = 3; i <= 9; i++)
        {
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "A" });
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "B" });
            modelBuilder.Entity<ClimbingLevel>().HasData(new ClimbingLevel { Level = i + "C" });
        }
        //modelBuilder.Entity<Climber>()
        //    .HasMany(e => e.Matches);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole("Administrator"));
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole("Moderator"));
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole("Climber"));
    }
}
