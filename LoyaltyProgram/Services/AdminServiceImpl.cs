using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class AdminServiceImpl : AdminService
    {
        private readonly DatabaseContext _databaseContext;
        public AdminServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool GetByEmail(string email)
        {
            return _databaseContext.Admins.FirstOrDefault(a => a.Email == email) != null;
        }
    }
}
