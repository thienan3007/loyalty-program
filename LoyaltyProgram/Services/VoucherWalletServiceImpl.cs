using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class VoucherWalletServiceImpl : VoucherWalletService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly VoucherDefinitionService voucherDefinitionService;
        private ISortHelper<VoucherWallet> _sortHelper;
        public VoucherWalletServiceImpl(DatabaseContext databaseContext, VoucherDefinitionService voucherDefinitionService, ISortHelper<VoucherWallet> sortHelper)
        {
            _databaseContext = databaseContext;
            this.voucherDefinitionService = voucherDefinitionService;
            _sortHelper = sortHelper;
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

        public PagedList<VoucherWallet> GetVoucherWallets(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<VoucherWallet> voucherWallets;
            if (filterString != null)
            {
                voucherWallets = _databaseContext.VoucherWallets.Where(b => b.Status == 1 && _databaseContext.VoucherDefinitions.First(v => v.Id == b.VoucherDefinitionId).Name.Contains(filterString));
            }
            else
            {
                voucherWallets = _databaseContext.VoucherWallets.Where(b => b.Status == 1);
            }

            var sortedWallet = _sortHelper.ApplySort(voucherWallets, pagingParameters.OrderBy);
            if (voucherWallets != null)
            {
                return PagedList<VoucherWallet>.ToPagedList(sortedWallet, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
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

        public PagedList<VoucherWallet> GetVoucherOfMember(int membershipId, PagingParameters pagingParameters)
        {
            var voucherWalletList = PagedList<VoucherWallet>.ToPagedList(_databaseContext.VoucherWallets.Where(v => v.MembershipId == membershipId).OrderBy(c => c.MembershipId), pagingParameters.PageNumber, pagingParameters.PageSize);
            foreach (VoucherWallet voucherWallet in voucherWalletList) {
                voucherWallet.VoucherDefinition = this.voucherDefinitionService.GetVoucher(voucherWallet.VoucherDefinitionId);
            }
            return voucherWalletList;
        }
    }
}
