using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface MemberTierService
    {
        public List<MemberTier> GetMemberTiers();
        public MemberTier GetMemberTierByID(int id);
        public bool AddMemberTier(MemberTier memberTier);
        public bool UpdateMemberTier(MemberTier memberTier, int id);
        public bool DeleteMemberTier(int id);
        public int GetCount();
    }
}
