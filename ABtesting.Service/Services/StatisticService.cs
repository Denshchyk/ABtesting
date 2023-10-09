using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class StatisticService : IStatisticService
{
    private ApplicationContext _context;

    public StatisticService(ApplicationContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Calculates and returns the distribution of experiments by their key and value.
    /// </summary>
    /// <returns>
    /// A list of objects representing the distribution of experiments by key and value.
    /// Each object contains properties Key, Value, and Count.
    /// </returns>
    public async Task<List<DistributionModel>> DistributionByKeyAndValueAsync()
    {
        var distributionResults = _context.DevicesExperiments
            .GroupBy(x => new { x.Experiment.Key, x.Experiment.Value })
            .Select(group => new DistributionModel(group.Key.Key, group.Key.Value, group.Count()));

        return await distributionResults.ToListAsync();
    }
    
    /// <summary>
    /// Retrieves a list of all experiments from the database.
    /// </summary>
    /// <returns>A list of <see cref="Experiment"/> objects representing all experiments.</returns>
    public List<Experiment> GetAllExperimentsToList()
    {
        return _context.Experiments.ToList();
    }
    
    /// <summary>
    /// Gets the total count of devices including their associated experiments.
    /// </summary>
    /// <returns>The total count of devices.</returns>
    public int GetAllDevices()
    {
        return _context.Devices.Include(x => x.DevicesExperiments).Count();
    }
    
    /// <summary>
    /// Retrieves a list of all experiments including their associated information asynchronously.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{ExperimentModel}"/> representing all experiments and their details.</returns>
    public async Task<IEnumerable<ExperimentModel>> GetAllExperiments()
    {
        var experiments = await _context.Experiments.Include(x => x.DevicesExperiments).ToListAsync();
        return experiments.Select(x => new ExperimentModel(x.Id, x.Key, x.Value, x.ChanceInPercents));
    }
}