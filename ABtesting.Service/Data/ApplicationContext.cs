using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Experiment>()
            .HasMany(e => e.Devices)
            .WithMany(d => d.Experiments)
            .UsingEntity<DevicesExperiment>();
        
        modelBuilder.Entity<DevicesExperiment>()
            .HasKey(t => new { t.ExperimentId, t.DeviceToken });
    }

    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DevicesExperiment> DevicesExperiments { get; set; }
}