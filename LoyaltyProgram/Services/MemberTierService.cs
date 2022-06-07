using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface MemberTierService
    {
        public List<MemberTier> GetMemberTiers();
        public List<MemberTier> GetMemberTiers(int id);
        public MemberTier GetMemberTier(int loyaltyMemberId, int loyaltyTierID);
        public bool AddMemberTier(MemberTier memberTier);
        public bool UpdateMemberTier(MemberTier memberTier, int loyaltyMemberId, int loyaltyTierId);
        public bool DeleteMemberTier(int loyaltyMemberId, int loyaltyTierID);
        public int GetCount();
    }
}
