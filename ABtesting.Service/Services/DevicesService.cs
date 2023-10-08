using Microsoft.EntityFrameworkCore;

namespace ABtesting.Service;

public class DevicesService : IDevicesService
{
    private ApplicationContext _context;

    public DevicesService(ApplicationContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Adds a new device asynchronously to the database.
    /// </summary>
    /// <param name="addDevice">The device to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddDeviceAsync(Device addDevice)
    {
        await _context.Devices.AddAsync(addDevice);
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Retrieves a device by its unique device token asynchronously.
    /// </summary>
    /// <param name="deviceToken">The unique identifier of the device to retrieve.</param>
    /// <returns>
    /// The <see cref="Device"/> object representing the device with the specified token if found;
    /// otherwise, returns <c>null</c>.
    /// </returns>
    public async Task<Device?> GetByDeviceTokenAsync(Guid deviceToken)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(device => device.DeviceToken == deviceToken);
        return device;
    }

    /// <summary>
    /// Retrieves a list of all devices and their associated information asynchronously.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{DeviceModel}"/> representing all devices and their details.</returns>
    public async Task<IEnumerable<DeviceModel>> GetAllDevices()
    {
        var devices = _context.Devices.Include(x => x.DevicesExperiments).ToList();
        return devices.Select(x => new DeviceModel(x.DeviceToken, x.Type));
    }
}