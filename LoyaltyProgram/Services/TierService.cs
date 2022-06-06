using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface TierService
    {
        public List<Tier> GetTiers();
        public Tier GetTierByID(int id);
        public bool AddTier(Tier tier);
        public bool UpdateTier(Tier tier, int id);
        public bool DeleteTier(int id);
        public int GetTierCount();
    }
}
