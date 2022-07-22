using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface DeviceService
    {
        public bool Add(Device device);
        public bool Remove(Device device);
        public List<Device> GetDevices(int? accountId);

    }
}
