using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class StatisticService : IStatisticService
{
    private ApplicationContext _context;

    public StatisticService(ApplicationContext context)
    {
        _context = context;
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
    
    public int GetAllDevices()
    {
        return _context.Devices.Include(x => x.DevicesExperiments).Count();
    }
    public async Task<IEnumerable<ExperimentModel>> GetAllExperiments()
    {
        var experiments = await _context.Experiments.Include(x => x.DevicesExperiments).ToListAsync();
        return experiments.Select(x => new ExperimentModel(x.Id, x.Key, x.Value, x.ChanceInPercents));
    }
}