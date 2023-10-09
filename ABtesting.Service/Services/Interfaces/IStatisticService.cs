namespace ABtesting.Service;

public interface IStatisticService
{
    Task<List<DistributionModel>> DistributionByKeyAndValueAsync();
    int GetAllDevices();
    Task<IEnumerable<ExperimentModel>> GetAllExperiments();
}