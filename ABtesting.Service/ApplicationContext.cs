using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DevicesExperiment> DevicesExperiments { get; set; }
}