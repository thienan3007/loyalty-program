using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class TierServiceImpl : TierService
    {
        private readonly DatabaseContext databaseContext;
        public TierServiceImpl(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public bool AddTier(Tier tier)
        {
            if (tier != null)
            {
                databaseContext.Tiers.Add(tier);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteTier(int id)
        {
            var tier = databaseContext.Tiers.FirstOrDefault(b => b.Id == id);
            if (tier != null)
            {
                if (tier.Status == 1)
                {
                    tier.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public List<Tier> GetTiers()
        {
            return databaseContext.Tiers.Where(t => t.Status == 1).ToList();
        }

        public Tier GetTierByID(int id)
        {
            var tier = databaseContext.Tiers.FirstOrDefault(t => t.Id == id);
            if (tier != null)
            {
                if (tier.Status == 1)
                {
                    return tier;
                }
            }
            return null;
        }

        public int GetTierCount()
        {
            return databaseContext.Tiers.Count(); 
        }

        public bool UpdateTier(Tier tier, int id)
        {
            if (tier != null)
            {
                var tierDB = databaseContext.Tiers.FirstOrDefault(t => t.Id == id);
                if (tierDB != null)
                {
                    if (tierDB.Status == 1)
                    {
                        if (tier.Name != null)
                            tierDB.Name = tier.Name;
                        if (tier.Status != null)
                            tierDB.Status = tier.Status;
                        if (tier.LoyaltyProgramId != null)
                            tierDB.LoyaltyProgramId = tier.LoyaltyProgramId;
                        if (tierDB.SequenceNumber != null)
                            tierDB.SequenceNumber = tier.SequenceNumber;
                        if (tier.MinPoints != null)
                            tierDB.MinPoints = tier.MinPoints;
                        if (tier.RatioPoints != null)
                            tierDB.RatioPoints = tier.RatioPoints;
                        if (tier.Description != null)
                            tierDB.Description = tier.Description;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
