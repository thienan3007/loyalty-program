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
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                if (membership.Status == 1)
                {
                    membership.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public int GetCount()
        {
            return databaseContext.Memberships.Where(m => m.Status == 1).Count();
        }

        public Membership GetMembershipById(int id)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                return membership;
            }
            return null;
        }

        public List<Membership> GetMemberships()
        {
            return databaseContext.Memberships.Where(m => m.Status == 1).ToList();
        }

        public bool UpdateMembership(Membership membership, int id)
        {
            var membershipDb = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                if (membershipDb != null)
                {
                    if (membershipDb.Status == 1)
                    {
                        if (membership.MembershipEndDate != null)
                            membershipDb.MembershipEndDate = membership.MembershipEndDate;
                        if (membership.CanReceivePromotions != null)
                            membershipDb.CanReceivePromotions = membership.CanReceivePromotions;
                        if (membership.Status != null)
                            membershipDb.Status = membership.Status;
                        if (membership.LastTransactionDate != null)
                            membershipDb.LastTransactionDate = membership.LastTransactionDate;
                        if (membership.Description != null)
                            membershipDb.Description = membership.Description;
                        if (membership.EnrollmenDate != null)
                            membershipDb.EnrollmenDate = membership.EnrollmenDate;
                        if (membership.LoyaltyProgramId != null)
                            membershipDb.LoyaltyProgramId = membership.LoyaltyProgramId;
                        if (membership.MembershipCode != null)
                            membershipDb.MembershipCode = membership.MembershipCode;
                        if (membership.ReferrerMemberId != null)
                            membershipDb.ReferrerMemberId = membership.ReferrerMemberId;
                        if (membership.ReferrerMemberDate != null)
                            membershipDb.ReferrerMemberDate = membership.ReferrerMemberDate;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }
            return false;
        }
    }
}
