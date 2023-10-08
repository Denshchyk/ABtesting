namespace ABtesting.Service;

public interface IExperimentService
{
    Task AddExperimentAsync(Experiment addExperiment);
    Task<Experiment?> GetByKeyAsync(string key);
}