using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class DeviceServiceImpl : DeviceService
    {
        private readonly DatabaseContext _databaseContext;

        public DeviceServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Device device)
        {
            _databaseContext.Devices.Add(device);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Device device)
        {
            _databaseContext.Devices.Remove(device);
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Device> GetDevices(int? accountId)
        {
            return _databaseContext.Devices.Where(d => d.AccountId == accountId).ToList();
        }

        public List<Device> GetDevices(int accountId)
        {
            return _databaseContext.Devices.Where(d => d.AccountId == accountId).ToList();
        }
    }
}
