using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface MemberReferrerLevelService
    {
        public List<MemberReferrerLevel> GetMemberReferrerLevels();
        public MemberReferrerLevel GetMemberReferrerLevel(int id);
        public bool AddMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel);
        public bool UpdateMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel, int id);
        public bool DeleteMemberReferrerLevel(int id);
        public int GetCount();
    }
}
