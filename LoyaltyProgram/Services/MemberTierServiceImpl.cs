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

        public bool DeleteMemberTier(int loyaltyMemberId, int loyaltyTierID)
        {
            var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierID));
            if (memberTierDb != null)
            {
                if (memberTierDb.Status == 1)
                {
                    memberTierDb.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;

        }

        public int GetCount()
        {
            return databaseContext.MemberTiers.Where(m => m.Status == 1).Count();
        }

        public MemberTier GetMemberTier(int loyaltyMemberId, int loyaltyTierID)
        {
            var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierID));
            if (memberTierDb != null)
            {
                if (memberTierDb.Status == 1)
                {
                    return memberTierDb;
                }
            }
            return null;
        }

        public List<MemberTier> GetMemberTiers(int loyaltyMemberId)
        {
            return databaseContext.MemberTiers.Where(m => m.LoyaltyMemberId == loyaltyMemberId).ToList();
        }

        public List<MemberTier> GetMemberTiers()
        {
            return databaseContext.MemberTiers.ToList();
        }

        public bool UpdateMemberTier(MemberTier memberTier, int loyaltyMemberId, int loyaltyTierId)
        {
            var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierId));
            if (memberTierDb != null)
            {
                if (memberTierDb.Status == 1)
                {
                    if (memberTier.Name != null)
                        memberTierDb.Name = memberTier.Name;
                    if (memberTier.EffectiveDate != null)
                        memberTierDb.EffectiveDate = memberTier.EffectiveDate;
                    if (memberTier.ExpirationDate != null)
                        memberTierDb.ExpirationDate = memberTier.ExpirationDate;
                    if (memberTier.UdpateTierDate != null)
                        memberTierDb.UdpateTierDate = memberTierDb.UdpateTierDate;
                    if (memberTier.Status != null)
                        memberTierDb.Status = memberTier.Status;
                    if (memberTier.Description != null)
                        memberTierDb.Description = memberTier.Description;

                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }
    }
}
