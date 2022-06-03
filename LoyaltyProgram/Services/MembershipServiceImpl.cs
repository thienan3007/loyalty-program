using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class MembershipServiceImpl : MembershipService
    {
        private DatabaseContext databaseContext;
        public MembershipServiceImpl(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public bool AddMembership(Membership membership)
        {
            if (membership != null)
            {
                databaseContext.Memberships.Add(membership);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteMembership(int id)
        {
            return false;
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Membership GetMembershipById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Membership> GetMemberships()
        {
            throw new NotImplementedException();
        }

        public bool UpdateMembership(Membership membership, int id)
        {
            throw new NotImplementedException();
        }
    }
}
