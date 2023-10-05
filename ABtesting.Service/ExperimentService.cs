using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class ExperimentService
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
}