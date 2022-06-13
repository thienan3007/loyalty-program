using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class AuthServiceImpl : AuthService
    {

        private readonly DatabaseContext databaseContext;

        public AuthServiceImpl(DatabaseContext database)
        {
            databaseContext = database;
        }

        public bool CheckExisted(string email)
        {
            return databaseContext.Memberships.FirstOrDefault(m => m.Email == email) != null;
        }

        public Membership GetMembership(string email)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.Email == email);

            if (membership != null)
            {
                return membership;
            }
            return null;
        }

        public Membership Login(string email)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.Email == email);

            if (membership != null)
            {
                return membership;
            }
            return null;
        }

        public bool RemoveRefreshToken(string email)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.Email == email);
            if (membership != null)
            {
                membership.RefreshToken = null;
                membership.RefreshTokenExpiryTime = null;

                return databaseContext.SaveChanges() > 0;
            }

            return false;
        }

        public bool RevokeAll()
        {
            var membershipList = databaseContext.Memberships.ToList();
            foreach (Membership membership in membershipList)
            {
                membership.RefreshToken = null;
            }

            return databaseContext.SaveChanges() > 0;
        }

        public bool UpdateRefreshToken(string email, string token, DateTime expired)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.Email == email);
            if (membership != null)
            {
                membership.RefreshToken = token;
                membership.RefreshTokenExpiryTime = expired;

                return databaseContext.SaveChanges() > 0;
            }

            return false;
        }
    }
}
