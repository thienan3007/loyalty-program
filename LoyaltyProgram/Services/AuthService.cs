using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface AuthService
    {
        public Membership Login(string email);
        public bool CheckExisted(string email);

        public Membership GetMembership(string email);
        public bool RevokeAll();

        public bool RemoveRefreshToken(string email);
        public bool UpdateRefreshToken(string email, string token, DateTime expired);
    }
}
