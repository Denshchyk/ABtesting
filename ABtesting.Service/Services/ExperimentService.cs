using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class ExperimentService : IExperimentService
{
    private ApplicationContext _context;

    public ExperimentService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddExperimentAsync(Experiment addExperiment)
    {
        var existingExperiment = _context.Experiments.Where(e => e.Key == addExperiment.Key);

        if (await existingExperiment.AnyAsync())
        {
            var totalChance = _context.Experiments
                .Where(e => e.Key == addExperiment.Key)
                .Sum(e => e.ChanceInPercents);
            
            if (totalChance + addExperiment.ChanceInPercents > 100)
            {
                throw new Exception("The total ChanceInPercents for experiments with the same key would exceed 100.");
            }
        }
        await _context.Experiments.AddAsync(addExperiment);
        await _context.SaveChangesAsync();
    }
    public async Task<Experiment?> GetByKeyAsync(string key)
    {
        var experiment = await _context.Experiments.FirstOrDefaultAsync(experiment => experiment.Key == key);
        return experiment;
    }
}