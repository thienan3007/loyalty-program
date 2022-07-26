using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface ReferrerService
    {
        public Membership AddReferrer(string memberCode, int accountId);

        public bool CheckReferrer(int accountId);
    }
}
