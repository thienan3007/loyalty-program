using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface MemberReferrerLevelService
    {
        public PagedList<MemberReferrerLevel> GetMemberReferrerLevels(PagingParameters pagingParameters);
        public MemberReferrerLevel GetMemberReferrerLevel(int id);
        public bool AddMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel);
        public bool UpdateMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel, int id);
        public bool DeleteMemberReferrerLevel(int id);
        public int GetCount();
    }
}
