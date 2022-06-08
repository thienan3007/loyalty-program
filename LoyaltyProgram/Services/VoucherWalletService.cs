using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface VoucherWalletService
    {
        public List<VoucherWallet> GetVoucherWallets();
        public VoucherWallet GetVoucherWallet(int membershipId, int voucherDefinitionId);
        public bool Add(VoucherWallet voucherWallet);
        public bool Update(VoucherWallet voucherWallet, int membershipId, int voucherDefinitionId);
        public bool Delete(int membershipId, int voucherDefinitionId);
        public int GetCount();
    }
}
