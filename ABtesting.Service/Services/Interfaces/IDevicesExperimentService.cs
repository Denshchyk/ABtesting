namespace ABtesting.Service;

public interface IDevicesExperimentService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<ExperimentModel> AddRandomExperimentToDeviceAsync(Guid deviceToken, string key);
    Dictionary<string, int> NumberOfDevicesByKey(List<Experiment> experiments);
    List<object> DistributionByKeyAndValue(List<Experiment> experiments); 
    Task<ExperimentModel?> GetAllExperimentsForDeviceAsync(Guid deviceToken, string key);
}