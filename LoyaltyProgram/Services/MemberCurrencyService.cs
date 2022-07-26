using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface MemberCurrencyService
    {
        public PagedList<MembershipCurrency> GetMembershipCurrencies(PagingParameters pagingParameters);
        public MembershipCurrency GetMembershipCurrency(int membershipID, int currencyId);
        public bool AddMembershipCurrency(MembershipCurrency membershipCurrency);
        public bool UpdateMembershipCurrency(MembershipCurrency membershipCurrency, int membershipId, int currencyId);
        public bool DeleteMembershipCurrency(int id);
        public int GetCount();
    }
}
