using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface VoucherDefinitionService
    {
        public PagedList<VoucherDefinition> GetVouchers(PagingParameters pagingParameters);
        public List<VoucherDefinition> GetVouchers(int accountId);
        public VoucherDefinition GetVoucher(int? id);
        public bool AddVoucher(VoucherDefinition voucherDefinition);
        public bool UpdateVoucher(VoucherDefinition voucherDefinition, int id);
        public bool DeleteVoucher(int id);
        public int GetCount();
    }
}
