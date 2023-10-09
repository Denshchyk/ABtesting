using ABtesting.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ABtesting.Web;

[ApiController]
[Route("api/[controller]")]
public class ExperimentController : ControllerBase
{
    private readonly IExperimentService _experimentService;
    private readonly IDevicesService _devicesService;
    private readonly IDevicesExperimentService _devicesExperimentService;

    public ExperimentController(IExperimentService experimentService, IDevicesService devicesService,
        IDevicesExperimentService devicesExperimentService)
    {
        _experimentService = experimentService;
        _devicesService = devicesService;
        _devicesExperimentService = devicesExperimentService;
    }
    
    [HttpGet("{key}")]
    public async Task<ActionResult<ExperimentModel>> GetExperiment(string key, [FromQuery]Guid deviceToken)
    {
        var device = await _devicesService.GetByDeviceTokenAsync(deviceToken);

        if (device is null)
        {
            var addDevice = new Device { DeviceToken = deviceToken, Type = "device.Type"};
            await _devicesService.AddDeviceAsync(addDevice);

            var randomExperiment = await _devicesExperimentService.AddRandomExperimentToDeviceAsync(addDevice.DeviceToken, key);
            return Created(nameof(GetExperiment), randomExperiment);
        }

        var devicesExperiments = await _devicesExperimentService.GetAllExperimentsForDeviceAsync(deviceToken, key);
        
        if (devicesExperiments is not null)
        {
            return devicesExperiments;
        }
        return NoContent();
    }
    
    [HttpPost]
    public async Task AddExperiment(string key, string value, int chanceInPrecents)
    {
        if (chanceInPrecents > 100)
        {
            BadRequest("The total ChanceInPercents for experiments would exceed 100.");
        }
        var addExperiment = new Experiment { Key = key, Value = value, ChanceInPercents = chanceInPrecents};
        await _experimentService.AddExperimentAsync(addExperiment);
    }
}