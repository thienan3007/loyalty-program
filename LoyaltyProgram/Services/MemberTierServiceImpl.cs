using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class MemberTierServiceImpl : MemberTierService
    {

        private readonly DatabaseContext databaseContext;
        public MemberTierServiceImpl(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public bool AddMemberTier(MemberTier memberTier)
        {
            if (memberTier != null)
            {
                databaseContext.MemberTiers.Add(memberTier);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteMemberTier(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public MemberTier GetMemberTierByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<MemberTier> GetMemberTiers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateMemberTier(MemberTier memberTier, int id)
        {
            throw new NotImplementedException();
        }
    }
}
