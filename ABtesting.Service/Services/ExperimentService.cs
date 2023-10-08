using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class ExperimentService : IExperimentService
{
    private ApplicationContext _context;

    public ExperimentService(ApplicationContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Adds a new experiment asynchronously to the database while ensuring that the total
    /// chance in percents for experiments with the same key does not exceed 100%.
    /// </summary>
    /// <param name="addExperiment">The experiment to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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
    
    /// <summary>
    /// Retrieves an experiment by its unique key asynchronously.
    /// </summary>
    /// <param name="key">The key of the experiment to retrieve.</param>
    /// <returns>
    /// The <see cref="Experiment"/> object representing the experiment with the specified key if found;
    /// otherwise, returns <c>null</c>.
    /// </returns>
    public async Task<Experiment?> GetByKeyAsync(string key)
    {
        var experiment = await _context.Experiments.FirstOrDefaultAsync(experiment => experiment.Key == key);
        return experiment;
    }
}