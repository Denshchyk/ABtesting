namespace ABtesting.Service;

public interface IStatisticService
{
    List<object> DistributionByKeyAndValue(List<Experiment> experiments);
    int GetAllDevices();
    Task<IEnumerable<ExperimentModel>> GetAllExperiments();
    List<Experiment> GetAllExperimentsToList();
}