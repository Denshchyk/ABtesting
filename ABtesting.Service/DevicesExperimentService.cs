using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class DevicesExperimentService
{
    private ApplicationContext _context;

    public DevicesExperimentService(ApplicationContext context)
    {
        _context = context;
    }
    // количество девайсов, которые учавствуют в каждом из экспериментов
    public static Dictionary<string, int> NumberOfDevicesByKey(List<Experiment> experiments)
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
    
    public static List<object> DistributionByKeyAndValue(List<Experiment> experiments)
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