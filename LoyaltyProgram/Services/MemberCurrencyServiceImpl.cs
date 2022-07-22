using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Services
{
    public class MemberCurrencyServiceImpl : MemberCurrencyService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly MembershipService _membershipService;
        private ISortHelper<MembershipCurrency> _sortHelper;
        public MemberCurrencyServiceImpl(DatabaseContext databaseContext, MembershipService membershipService, ISortHelper<MembershipCurrency> sortHelper)
        {
            _databaseContext = databaseContext;
            _membershipService = membershipService;
            _sortHelper = sortHelper;
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

        public MembershipCurrency GetMembershipCurrency(int membershipId, int currencyId)
        {
            var membershipCurrency = _databaseContext.MembershipCurrencies.FirstOrDefault(b => b.MembershipId == membershipId && b.CurrencyId == currencyId);
            if (membershipCurrency != null)
            {
                if (membershipCurrency.Status == 1)
                {
                    membershipCurrency.Membership = _membershipService.GetMembershipById(membershipId);
                    return membershipCurrency;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.MembershipCurrencies.Where(b => b.Status == 1).Count();
        }

        public PagedList<MembershipCurrency> GetMembershipCurrencies(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<MembershipCurrency> currencies;
            if (filterString != null)
            {
                currencies =_databaseContext.MembershipCurrencies.Where(b => b.Status == 1 && b.Name.Contains(filterString));
            }
            else
            {
                currencies = _databaseContext.MembershipCurrencies.Where(b => b.Status == 1);
            }

            var sortedCurrencies = _sortHelper.ApplySort(currencies, pagingParameters.OrderBy);
            if (currencies != null)
            {
                return PagedList<MembershipCurrency>.ToPagedList(sortedCurrencies, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateMembershipCurrency(MembershipCurrency membershipCurrency, int membershipId, int currencyId)
        {
            if (membershipCurrency != null)
            {
                var membershipCurrencyDb = GetMembershipCurrency(membershipId, currencyId);
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

                        _databaseContext.Entry(membershipCurrencyDb).State = EntityState.Modified;
                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
