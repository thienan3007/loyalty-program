using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface MemberCurrencyService
    {
        public List<MembershipCurrency> GetMembershipCurrencies();
        public MembershipCurrency GetMembershipCurrency(int id);
        public bool AddMembershipCurrency(MembershipCurrency membershipCurrency);
        public bool UpdateMembershipCurrency(MembershipCurrency membershipCurrency, int id);
        public bool DeleteMembershipCurrency(int id);
        public int GetCount();
    }
}
