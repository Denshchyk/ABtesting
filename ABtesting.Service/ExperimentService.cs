using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public interface IExperimentService
{
    Task AddExperimentAsync(Experiment addExperiment);
    Task<Experiment?> GetByKeyAsync(string key);
    List<Experiment> GetAllExperiments();
}

public class ExperimentService : IExperimentService
{
    private ApplicationContext _context;

    public ExperimentService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddExperimentAsync(Experiment addExperiment)
    {
        await _context.Experiments.AddAsync(addExperiment);
        await _context.SaveChangesAsync();
    }
    public async Task<Experiment?> GetByKeyAsync(string key)
    {
        var experiment = await _context.Experiments.FirstOrDefaultAsync(experiment => experiment.Key == key);
        return experiment;
    }
    public List<Experiment> GetAllExperiments()
    {
        return _context.Experiments.ToList();
    }
}