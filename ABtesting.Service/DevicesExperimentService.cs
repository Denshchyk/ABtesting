using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public interface IDevicesExperimentService
{
    public Task<ExperimentModel> AddRandomExperimentToDeviceAsync(Guid deviceToken, string key);
    Dictionary<string, int> NumberOfDevicesByKey(List<Experiment> experiments);
    List<object> DistributionByKeyAndValue(List<Experiment> experiments);
    public IEnumerable<DevicesExperiment> GetAllExperimentsForDevice(Guid deviceToken);
}

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

    public IEnumerable<DevicesExperiment> GetAllExperimentsForDevice(Guid deviceToken)
    {
        return _context.DevicesExperiments.Where(de => de.DeviceToken == deviceToken);
    }
    
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
    // количество девайсов, которые учавствуют в каждом из экспериментов
    public Dictionary<string, int> NumberOfDevicesByKey(List<Experiment> experiments)
    {
        var numberOfDevicesByKey = experiments
            .SelectMany(exp => exp.DevicesExperiments)
            .GroupBy(de => de.ExperimentId)
            .ToDictionary(
                group => experiments.First(exp => exp.Id == group.Key).Key,
                group => group.Count()
            );
        
        return numberOfDevicesByKey;
    }
    
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
}