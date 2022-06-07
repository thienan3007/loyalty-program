using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface VoucherDefinitionService
    {
        public List<VoucherDefinition> GetVouchers();
        public VoucherDefinition GetVoucher(int id);
        public bool AddVoucher(VoucherDefinition voucherDefinition);
        public bool UpdateVoucher(VoucherDefinition voucherDefinition, int id);
        public bool DeleteVoucher(int id);
        public int GetCount();
    }
}
