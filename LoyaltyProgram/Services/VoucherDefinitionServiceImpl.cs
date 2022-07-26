using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class VoucherDefinitionServiceImpl : VoucherDefinitionService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<VoucherDefinition> _sortHelper;
        public VoucherDefinitionServiceImpl(DatabaseContext databaseContext, ISortHelper<VoucherDefinition> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
        }

        public bool AddVoucher(VoucherDefinition voucherDefinition)
        {

            _databaseContext.VoucherDefinitions.Add(voucherDefinition);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteVoucher(int id)
        {
            var voucher = _databaseContext.VoucherDefinitions.FirstOrDefault(b => b.Id == id);
            if (voucher != null)
            {
                if (voucher.Status == 1)
                {
                    voucher.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public List<VoucherDefinition> GetVouchers(int accountId)
        {
            //get all voucher 
            var vouchers = _databaseContext.VoucherDefinitions.Where(v => v.Status == 1).ToList();
            //get voucher wallet
            var wallets = _databaseContext.VoucherWallets.Where(w => w.MembershipId == accountId).ToList();

            var count = 0;

            for (int i = 0; i < vouchers.Count; i++)
            {
                var voucher = vouchers.ElementAt(i);
                for (int j = 0; j < wallets.Count; j++)
                {
                    var wallet = wallets.ElementAt(j);
                    if (voucher.Id == wallet.VoucherDefinitionId)
                    {
                        vouchers.RemoveAt(i);
                        count++;
                        if (count == wallets.Count)
                        {
                            break;
                        }
                    }
                }
            }

            return vouchers;
        }

        public VoucherDefinition GetVoucher(int? id)
        {
            var voucher = _databaseContext.VoucherDefinitions.FirstOrDefault(b => b.Id == id);
            if (voucher != null)
            {
                if (voucher.Status == 1)
                {
                    return voucher;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.VoucherDefinitions.Where(b => b.Status == 1).Count();
        }

        public PagedList<VoucherDefinition> GetVouchers(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<VoucherDefinition> voucherDefinitions;
            if (filterString != null)
            {
                voucherDefinitions =
                    _databaseContext.VoucherDefinitions.Where(v => v.Status == 1 && v.Name.Contains(filterString));
            }
            else
            {
                voucherDefinitions =_databaseContext.VoucherDefinitions.Where(b => b.Status == 1);
            }

            var sortedList = _sortHelper.ApplySort(voucherDefinitions, pagingParameters.OrderBy);
            if (voucherDefinitions != null)
            {
                return PagedList<VoucherDefinition>.ToPagedList(sortedList, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateVoucher(VoucherDefinition voucher, int id)
        {
            if (voucher != null)
            {
                var voucherDb = GetVoucher(id);
                if (voucherDb != null)
                {
                    if (voucherDb.Status == 1)
                    {
                        if (voucher.Name != null)
                            voucherDb.Name = voucher.Name;
                        if (voucher.DiscountValue != null)
                            voucherDb.DiscountValue = voucher.DiscountValue;
                        if (voucher.EffectiveDate != null)
                            voucherDb.EffectiveDate = voucher.EffectiveDate;
                        if (voucher.ExpirationDate != null)
                            voucherDb.ExpirationDate = voucher.ExpirationDate;
                        if (voucher.VoucherCode != null)
                            voucherDb.VoucherCode = voucher.VoucherCode;
                        if (voucher.Status != null)
                            voucherDb.Status = voucher.Status;
                        if (voucher.Description != null)
                            voucherDb.Description = voucher.Description;
                        if (voucher.ExpirationPeriod != null)
                            voucherDb.ExpirationPeriod = voucher.ExpirationPeriod;
                        if (voucher.ExpirationPeriodUnits != null)
                            voucherDb.ExpirationPeriodUnits = voucher.ExpirationPeriodUnits;
                        if (voucher.IsPartialRedeemable != null)
                            voucherDb.IsPartialRedeemable = voucher.IsPartialRedeemable;
                        if (voucher.Image != null)
                            voucherDb.Image = voucher.Image;
                        if (voucher.Point != null)
                            voucherDb.Point = voucher.Point;
                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
