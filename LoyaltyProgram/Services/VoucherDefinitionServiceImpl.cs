using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class VoucherDefinitionServiceImpl : VoucherDefinitionService
    {
        private readonly DatabaseContext _databaseContext;
        public VoucherDefinitionServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public VoucherDefinition GetVoucher(int id)
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

        public List<VoucherDefinition> GetVouchers()
        {
            return _databaseContext.VoucherDefinitions.Where(b => b.Status == 1).ToList();
        }

        public bool UpdateVoucher(VoucherDefinition voucher, int id)
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

                    return _databaseContext.SaveChanges() > 0;
                }
            }

            return false;
        }
    }
}
