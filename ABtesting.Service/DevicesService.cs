using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class DevicesService
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
}