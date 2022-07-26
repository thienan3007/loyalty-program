using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface TierService
    {
        public PagedList<Tier> GetTiers(PagingParameters pagingParameters);
        public PagedList<Tier> GetTiers(PagingParameters pagingParameters, int programId);
        public List<Tier> GetTiers();
        public List<Tier> GetTiers(int programId);
        public Tier GetTierByID(int? id);
        public bool AddTier(Tier tier);
        public bool UpdateTier(Tier tier, int id);
        public bool DeleteTier(int id);
        public int GetTierCount();
    }
}
