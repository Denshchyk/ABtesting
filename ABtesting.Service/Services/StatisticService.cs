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
    /// <param name="experiments">The list of experiments to calculate the distribution for.</param>
    /// <returns>
    /// A list of objects representing the distribution of experiments by key and value.
    /// Each object contains properties Key, Value, and Count.
    /// </returns>
    public List<object> DistributionByKeyAndValue(List<Experiment> experiments)
    {
        var distributionResults = experiments
            .SelectMany(exp => exp.DevicesExperiments.Select(de => new
            {
                exp.Key,
                exp.Value,
                DeviceToken = de.DeviceToken
            }))
            .GroupBy(x => new { x.Key, x.Value })
            .Select(group => new
            {
                Key = group.Key.Key,
                Value = group.Key.Value,
                Count = group.Count()
            })
            .ToList();

        return distributionResults.Cast<object>().ToList();
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