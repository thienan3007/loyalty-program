using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class MemberCurrencyServiceImpl : MemberCurrencyService
    {
        private readonly DatabaseContext _databaseContext;
        public MemberCurrencyServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddMembershipCurrency(MembershipCurrency membershipCurrency)
        {

            _databaseContext.MembershipCurrencies.Add(membershipCurrency);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteMembershipCurrency(int id)
        {
            var membershipCurrency = _databaseContext.MembershipCurrencies.FirstOrDefault(b => b.Id == id);
            if (membershipCurrency != null)
            {
                if (membershipCurrency.Status == 1)
                {
                    membershipCurrency.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public MembershipCurrency GetMembershipCurrency(int id)
        {
            var membershipCurrency = _databaseContext.MembershipCurrencies.FirstOrDefault(b => b.Id == id);
            if (membershipCurrency != null)
            {
                if (membershipCurrency.Status == 1)
                {
                    return membershipCurrency;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.MembershipCurrencies.Where(b => b.Status == 1).Count();
        }

        public List<MembershipCurrency> GetMembershipCurrencies()
        {
            return _databaseContext.MembershipCurrencies.Where(b => b.Status == 1).ToList();
        }

        public bool UpdateMembershipCurrency(MembershipCurrency membershipCurrency, int id)
        {
            if (membershipCurrency != null)
            {
                var membershipCurrencyDb = GetMembershipCurrency(id);
                if (membershipCurrencyDb != null)
                {
                    if (membershipCurrencyDb.Status == 1)
                    {
                        if (membershipCurrency.Name != null)
                            membershipCurrencyDb.Name = membershipCurrency.Name;
                        if (membershipCurrency.PointsBalance != null)
                            membershipCurrencyDb.PointsBalance = membershipCurrency.PointsBalance;
                        if (membershipCurrency.TotalPointsAccrued != null)
                            membershipCurrencyDb.TotalPointsAccrued = membershipCurrency.TotalPointsAccrued;
                        if (membershipCurrency.TotalPointsExpired != null)
                            membershipCurrencyDb.TotalPointsExpired = membershipCurrency.TotalPointsExpired;
                        if (membershipCurrency.TotalPointsRedeemed != null)
                            membershipCurrencyDb.TotalPointsRedeemed = membershipCurrency.TotalPointsRedeemed;
                        if (membershipCurrency.BalanceBeforeReset != null)
                            membershipCurrencyDb.BalanceBeforeReset = membershipCurrency.BalanceBeforeReset;
                        if (membershipCurrency.LastResetDate != null)
                            membershipCurrencyDb.LastResetDate = membershipCurrency.LastResetDate;
                        if (membershipCurrency.MembershipId != null)
                            membershipCurrencyDb.MembershipId = membershipCurrency.MembershipId;
                        if (membershipCurrency.ExpirationPoints != null)
                            membershipCurrencyDb.ExpirationPoints = membershipCurrency.ExpirationPoints;
                        if (membershipCurrency.CurrencyId != null)
                            membershipCurrencyDb.CurrencyId = membershipCurrencyDb.CurrencyId;
                        if (membershipCurrency.Status != null)
                            membershipCurrencyDb.Status = membershipCurrency.Status;
                        if (membershipCurrency.Description != null)
                            membershipCurrencyDb.Description = membershipCurrency.Description;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
