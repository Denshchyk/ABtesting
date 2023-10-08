using ABtesting.Service;
using Microsoft.AspNetCore.Mvc;

namespace ABtesting.Web;
[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDevicesService _devicesService;

    public DeviceController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }
    
    [HttpPost("{type}")]
    public async Task AddDevice(string type)
    {
        var addDevice = new Device { Type = type};
        await _devicesService.AddDeviceAsync(addDevice);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceModel>>> GetAllDevices()
    {
        var deviceModels = await _devicesService.GetAllDevices();
        return Ok(deviceModels);
    }
}
