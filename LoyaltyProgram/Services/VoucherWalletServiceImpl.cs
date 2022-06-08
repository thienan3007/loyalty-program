using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class VoucherWalletServiceImpl : VoucherWalletService
    {
        private readonly DatabaseContext _databaseContext;
        public VoucherWalletServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(VoucherWallet voucherWallet)
        {

            _databaseContext.VoucherWallets.Add(voucherWallet);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int membershipId, int voucherDefinitionId)
        {
            var voucherWallet = _databaseContext.VoucherWallets.FirstOrDefault(v => v.MembershipId == membershipId && v.VoucherDefinitionId == voucherDefinitionId);
            if (voucherWallet != null)
            {
                if (voucherWallet.Status == 1)
                {
                    voucherWallet.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public VoucherWallet GetVoucherWallet(int membershipId, int voucherDefinitionId)
        {
            var voucherWallet = _databaseContext.VoucherWallets.FirstOrDefault(v => v.MembershipId == membershipId && v.VoucherDefinitionId == voucherDefinitionId);
            if (voucherWallet != null)
            {
                if (voucherWallet.Status == 1)
                {
                    return voucherWallet;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.VoucherWallets.Where(b => b.Status == 1).Count();
        }

        public List<VoucherWallet> GetVoucherWallets()
        {
            return _databaseContext.VoucherWallets.Where(b => b.Status == 1).ToList();
        }

        public bool Update(VoucherWallet voucherWallet, int membershipId, int voucherDefinitionId)
        {
            var voucherWalletDb = GetVoucherWallet(membershipId, voucherDefinitionId);
            if (voucherWallet != null)
            {
                if (voucherWalletDb != null)
                {
                    if (voucherWalletDb.Status == 1)
                    {
                        if (voucherWallet.Status != null)
                            voucherWalletDb.Status = voucherWallet.Status;
                        if (voucherWallet.UseDate != null)
                            voucherWalletDb.UseDate = voucherWallet.UseDate;
                        if (voucherWallet.IsPartialRedeemable != null)
                            voucherWalletDb.IsPartialRedeemable = voucherWallet.IsPartialRedeemable;
                        if (voucherWallet.RedeemedValue != null)
                            voucherWalletDb.RedeemedValue = voucherWallet.RedeemedValue;
                        if (voucherWallet.RemainingValue != null)
                            voucherWalletDb.RemainingValue = voucherWallet.RemainingValue;
                        if (voucherWallet.Description != null)
                            voucherWalletDb.Description = voucherWallet.Description;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
