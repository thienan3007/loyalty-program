using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface MembershipService
    {
        public PagedList<Membership> GetMemberships(PagingParameters pagingParameters);
        public Membership GetMembershipById(int id);
        public Membership GetMembership(string email);
        public int AddMembership(Membership membership);
        public bool UpdateMembership(Membership membership, int id);
        public bool DeleteMembership(int id);
        public int GetCount();
    }
}
