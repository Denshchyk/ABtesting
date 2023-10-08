using System.ComponentModel.DataAnnotations;

namespace ABtesting.Service;

public class Device
{
    [Key]
    public Guid DeviceToken { get; set; }

    public string? Type { get; set; }

    public ICollection<Experiment> Experiments { get; set; }

    public ICollection<DevicesExperiment> DevicesExperiments { get; set; } = new List<DevicesExperiment>();
}