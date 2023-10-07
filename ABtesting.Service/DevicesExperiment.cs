namespace ABtesting.Service;
public class DevicesExperiment
{
    public Guid DeviceToken { get; set; }

    public Device Device { get; set; }
    public Guid ExperimentId { get; set; }

    public Experiment Experiment { get; set; }
}