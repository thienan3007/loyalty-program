using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;
namespace LoyaltyProgram.Services
{
    public interface VoucherWalletService
    {
        public PagedList<VoucherWallet> GetVoucherWallets(PagingParameters pagingParameters);
        public VoucherWallet GetVoucherWallet(int membershipId, int voucherDefinitionId);
        public PagedList<VoucherWallet> GetVoucherOfMember(int membershipId, PagingParameters pagingParameters);
        public bool Add(VoucherWallet voucherWallet);
        public bool Update(VoucherWallet voucherWallet, int membershipId, int voucherDefinitionId);
        public bool Delete(int membershipId, int voucherDefinitionId);
        public int GetCount();
    }
}
