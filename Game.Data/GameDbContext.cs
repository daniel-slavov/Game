using Game.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Game.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Choice> Choices { get; set; }
    public DbSet<GameRule> Rules { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Choice>().HasData(new List<Choice>
        {
            new()
            {
                Id = 1,
                Name = "Rock"
            },
            new()
            {
                Id = 2,
                Name = "Paper"
            },
            new()
            {
                Id = 3,
                Name = "Scissors"
            },
            new()
            {
                Id = 4,
                Name = "Lizard"
            },
            new()
            {
                Id = 5,
                Name = "Spock"
            }
        });

        builder.Entity<GameRule>().HasData(new List<GameRule>
        {
            new()
            {
                Id = 1,
                ChoiceId = 1,
                WinAgainstId = 2
            },
            new()
            {
                Id = 2,
                ChoiceId = 1,
                WinAgainstId = 4
            },
            new()
            {
                Id = 3,
                ChoiceId = 2,
                WinAgainstId = 1
            },
            new()
            {
                Id = 4,
                ChoiceId = 2,
                WinAgainstId = 5
            },
            new()
            {
                Id = 5,
                ChoiceId = 3,
                WinAgainstId = 2
            },
            new()
            {
                Id = 6,
                ChoiceId = 3,
                WinAgainstId = 4
            },
            new()
            {
                Id = 7,
                ChoiceId = 4,
                WinAgainstId = 2
            },
            new()
            {
                Id = 8,
                ChoiceId = 4,
                WinAgainstId = 5
            },
            new()
            {
                Id = 9,
                ChoiceId = 5,
                WinAgainstId = 3
            },
            new()
            {
                Id = 10,
                ChoiceId = 5,
                WinAgainstId = 1
            }
        });

        builder.Entity<GameRule>()
            .HasIndex(x => new
            {
                x.ChoiceId,
                x.WinAgainstId
            })
            .IsUnique();
    }
}