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
    Task<ExperimentModel?> GetAllExperimentsForDeviceAsync(Guid deviceToken, string key);
}