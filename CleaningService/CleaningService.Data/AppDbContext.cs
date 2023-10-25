using CleaningService.Repositories.Model;

namespace CleaningService.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CleaningRecord?> CleaningRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSnakeCaseNamingConvention();

        var parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION") ??
                                 configuration["DatabaseConnection"]);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        MapCleaningRecordColumns(modelBuilder);
    }

    private static void MapCleaningRecordColumns(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CleaningRecord>()
            .ToTable("cleaning_record");
        modelBuilder.Entity<CleaningRecord>()
            .Property(e => e.Id)
            .HasColumnName("id");
        modelBuilder.Entity<CleaningRecord>()
            .Property(e => e.Timestamp)
            .HasColumnName("timestamp");
        modelBuilder.Entity<CleaningRecord>()
            .Property(e => e.Commands)
            .HasColumnName("commands");
        modelBuilder.Entity<CleaningRecord>()
            .Property(e => e.Result)
            .HasColumnName("result");
        modelBuilder.Entity<CleaningRecord>()
            .Property(e => e.Duration)
            .HasColumnName("duration");
    }
}