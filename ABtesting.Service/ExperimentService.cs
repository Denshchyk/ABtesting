using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public interface IExperimentService
{
    Task AddExperimentAsync(Experiment addExperiment);
    Task<Experiment?> GetByKeyAsync(string key);
    Task<IEnumerable<ExperimentModel>> GetAllExperiments();
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
    public async Task<IEnumerable<ExperimentModel>> GetAllExperiments()
    {
        var experiments = await _context.Experiments.Include(x => x.DevicesExperiments).ToListAsync();
        return experiments.Select(x => new ExperimentModel(x.Id, x.Key, x.Value, x.ChanceInPercents));
    }
}