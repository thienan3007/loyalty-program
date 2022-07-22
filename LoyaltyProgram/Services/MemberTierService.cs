using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface MemberTierService
    {
        public PagedList<MemberTier> GetMemberTiers(PagingParameters pagingParameters);
        public PagedList<MemberTier> GetMemberTiers(int id, PagingParameters pagingParameters);
        public MemberTier GetMemberTier(int loyaltyMemberId, int loyaltyTierID);
        public MemberTier GetMemberTierCurrent(int loyaltyMemberId);
        public bool AddMemberTier(MemberTier memberTier);
        public bool UpdateMemberTier(MemberTier memberTier, int loyaltyMemberId, int loyaltyTierId);
        public bool DeleteMemberTier(int loyaltyMemberId, int loyaltyTierID);
        public int GetCount();
    }
}
