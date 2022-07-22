using LoyaltyProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Services
{
    public class ReferrerServiceImpl : ReferrerService
    {
        private readonly DatabaseContext context;

        public ReferrerServiceImpl(DatabaseContext databaseContext)
        {
            context = databaseContext;
        }
        public Membership AddReferrer(string memberCode, int accountId)
        {
            //load this account 
            var membership = context.Memberships.FirstOrDefault(m => m.AccountId == accountId);

            //load that account 
            var referrerMember = context.Memberships.FirstOrDefault(m => m.MembershipCode == memberCode);

            //check if that account is null
            if (referrerMember != null)
            {
                membership.ReferrerMemberId = referrerMember.AccountId;
                membership.ReferrerMemberDate = DateTime.Now;

                context.Entry(membership).State = EntityState.Modified;
                context.SaveChangesAsync();

                return referrerMember;
            }

            return null;
        }

        public bool CheckReferrer(int accountId)
        {
            //load this account 
            var membership = context.Memberships.FirstOrDefault(m => m.AccountId == accountId);

            if (membership != null)
            {
                if (membership.ReferrerMemberId != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
