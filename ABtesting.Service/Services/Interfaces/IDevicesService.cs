namespace ABtesting.Service;

public interface IDevicesService
{
    Task AddDeviceAsync(Device addDevice);
    Task<Device?> GetByDeviceTokenAsync(Guid deviceToken);
    Task<IEnumerable<DeviceModel>> GetAllDevices();
}