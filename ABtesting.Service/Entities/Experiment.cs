namespace ABtesting.Service;

public class Experiment
{
    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public int ChanceInPercents { get; set; }

    public ICollection<Device> Devices { get; set; }
    public ICollection<DevicesExperiment> DevicesExperiments { get; set; } = new List<DevicesExperiment>();
}