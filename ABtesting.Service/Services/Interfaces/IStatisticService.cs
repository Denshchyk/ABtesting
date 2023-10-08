namespace ABtesting.Service;

public interface IStatisticService
{
    Dictionary<string, int> NumberOfDevicesByKey(List<Experiment> experiments);
    List<object> DistributionByKeyAndValue(List<Experiment> experiments);
    int GetAllDevices();
    Task<IEnumerable<ExperimentModel>> GetAllExperiments();
}