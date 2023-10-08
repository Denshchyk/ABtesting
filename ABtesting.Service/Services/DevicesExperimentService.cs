using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class DevicesExperimentService : IDevicesExperimentService
{
    private ApplicationContext _context;
    private IRandomProvider _random;

    public DevicesExperimentService(ApplicationContext context, IRandomProvider random)
    {
        _context = context;
        _random = random;
    }
    public async Task<ExperimentModel> AddRandomExperimentToDeviceAsync(Guid deviceToken, string key)
    {
        var randomExperiment = GetRandomExperiment(key);
        var addDevicesExperiment = new DevicesExperiment {DeviceToken = deviceToken, ExperimentId = randomExperiment.Id};
        await _context.DevicesExperiments.AddAsync(addDevicesExperiment);
        await _context.SaveChangesAsync();
        return new ExperimentModel(randomExperiment.Id, randomExperiment.Key, randomExperiment.Value, randomExperiment.ChanceInPercents);
    }

    public async Task<ExperimentModel?> GetAllExperimentsForDeviceAsync(Guid deviceToken, string key)
    {
        var devicesExperiments = _context.DevicesExperiments.Include(x => x.Experiment)
            .Where(de => de.DeviceToken == deviceToken);
        if (await devicesExperiments.AnyAsync())
        {
            var experimentWithKey = devicesExperiments.FirstOrDefault(x=> x.Experiment.Key == key)!.Experiment;
            return new ExperimentModel(experimentWithKey.Id, experimentWithKey.Key, experimentWithKey.Value, experimentWithKey.ChanceInPercents);
        }
        return null;
    }
    
    /// <summary>
    /// Retrieves a random experiment based on its ChanceInPercents property.
    /// </summary>
    /// <param name="Key">The name of the experiment group to select from.</param>
    /// <returns>A randomly selected experiment based on the weighted ChanceInPercents.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the total chance for experiments with the given name is not 100% or if no experiment is selected.</exception>
    public Experiment GetRandomExperiment(string key)
    {
        var experiments = _context.Experiments
            .Where(e => e.Key == key)
            .ToList();

        if (experiments.Sum(e => e.ChanceInPercents) != 100m)
        {
            throw new InvalidOperationException("Total chance for experiments with the given name is not 100%.");
        }

        var cumulativeChance = 0m;
        var roll = (decimal)_random.NextDouble() * 100;

        foreach (var experiment in experiments)
        {
            cumulativeChance += experiment.ChanceInPercents;
            if (roll < cumulativeChance)
            {
                return experiment;
            }
        }
        
        throw new InvalidOperationException("No experiment selected. This should not happen.");
    }
}