using ABtesting.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ABtesting.Web;

[ApiController]
[Route("[controller]")]
public class DevicesExperimentController : ControllerBase
{
    private IExperimentService _experimentService;
    private readonly IDevicesService _devicesService;
    private readonly IDevicesExperimentService _devicesExperimentService;

    public DevicesExperimentController(IExperimentService experimentService, IDevicesService devicesService,
        IDevicesExperimentService devicesExperimentService)
    {
        _experimentService = experimentService;
        _devicesService = devicesService;
        _devicesExperimentService = devicesExperimentService;
    }

    [HttpGet("experiments")]
    public IEnumerable<Experiment> Get()
    {
        return _experimentService.GetAllExperiments();
    }

    [HttpGet("{experimentId}")]
    public async Task<ActionResult<DevicesExperiment>> GetDevicesById(Guid deviceToken, Guid experimentId)
    {
        var device = await _devicesService.GetByDeviceTokenAsync(deviceToken);

        if (device is null)
        {
            var addDevice = new Device { DeviceToken = deviceToken, Type = device.Type};
            await _devicesService.AddDeviceAsync(addDevice);

            await _devicesExperimentService.AddDevicesExperimentAsync(addDevice.DeviceToken, experimentId);
        }
        else
        {
            var devicesExperiment =
                device.DevicesExperiments.FirstOrDefault(de => de.DeviceToken == deviceToken && de.ExperimentId == experimentId);
            if (devicesExperiment is not null)
            {
                return devicesExperiment;
            }
        }
        return default;
    }
    [HttpPost("experiment")]
    public async Task AddExperiment(string key, string value, int chanceInPrecents)
    {
        var addExperiment = new Experiment { Key = key, Value = value, ChanceInPercents = chanceInPrecents};
        await _experimentService.AddExperimentAsync(addExperiment);
    }
    [HttpPost("devices/{type}")]
    public async Task AddDevice(string type)
    {
        var addDevice = new Device { Type = type};
        await _devicesService.AddDeviceAsync(addDevice);
    }
    [HttpGet("devices")]
    public IEnumerable<Device> GetAllDevices()
    {
        return _devicesService.GetAllDevices();
    }
}