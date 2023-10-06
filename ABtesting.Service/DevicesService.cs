using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public interface IDevicesService
{
    Task AddDeviceAsync(Device addDevice);
    Task<Device?> GetByDeviceTokenAsync(Guid deviceToken);
    List<Device> GetAllDevices();
}

public class DevicesService : IDevicesService
{
    private ApplicationContext _context;

    public DevicesService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddDeviceAsync(Device addDevice)
    {
        await _context.Devices.AddAsync(addDevice);
        await _context.SaveChangesAsync();
    }
    public async Task<Device?> GetByDeviceTokenAsync(Guid deviceToken)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(device => device.DeviceToken == deviceToken);
        return device;
    }

    public List<Device> GetAllDevices()
    {
        return _context.Devices.ToList();
    }
}