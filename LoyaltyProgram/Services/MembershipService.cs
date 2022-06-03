using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface MembershipService
    {
        public List<Membership> GetMemberships();
        public Membership GetMembershipById(int id);
        public bool AddMembership(Membership membership);
        public bool UpdateMembership(Membership membership, int id);
        public bool DeleteMembership(int id);
        public int GetCount();
    }
}
