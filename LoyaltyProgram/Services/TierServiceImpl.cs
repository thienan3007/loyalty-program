using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class TierServiceImpl : TierService
    {
        private readonly DatabaseContext databaseContext;
        private ISortHelper<Tier> _sortHelper;
        public TierServiceImpl(DatabaseContext databaseContext, ISortHelper<Tier> sortHelper)
        {
            this.databaseContext = databaseContext;
            _sortHelper = sortHelper;
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

        public PagedList<Tier> GetTiers(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Tier> tiers;
            if (filterString != null)
            {
                tiers = databaseContext.Tiers.Where(t => t.Status == 1 && t.Name.Contains(filterString));
            }
            else
            {
                tiers = databaseContext.Tiers.Where(t => t.Status == 1);
            }

            var sortedTiers = _sortHelper.ApplySort(tiers, pagingParameters.OrderBy);
            if (tiers != null)
            {
                return PagedList<Tier>.ToPagedList(sortedTiers, pagingParameters.PageNumber, pagingParameters.PageSize);
            }

            return null;
        }

        public Tier GetTierByID(int? id)
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

        public PagedList<Tier> GetTiers(PagingParameters pagingParameters, int programId)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Tier> tiers;
            if (filterString != null)
            {
                tiers = databaseContext.Tiers.Where(t => t.Status == 1 && t.LoyaltyProgramId == programId && t.Name.Contains(filterString));
            }
            else
            {
                tiers = databaseContext.Tiers.Where(t => t.Status == 1);
            }

            var sortedTiers = _sortHelper.ApplySort(tiers, pagingParameters.OrderBy);
            if (tiers != null)
            {
                return PagedList<Tier>.ToPagedList(sortedTiers, pagingParameters.PageNumber, pagingParameters.PageSize);
            }

            return null;
        }

        public List<Tier> GetTiers()
        {
            var tierList = databaseContext.Tiers.ToList();
            if (tierList != null)
            {
                return tierList;
            }
            return null;
        }

        public List<Tier> GetTiers(int programId)
        {
            var tierList = databaseContext.Tiers.Where(t => t.Status == 1 && t.LoyaltyProgramId == programId).ToList();
            if (tierList != null)
            {
                return tierList;

            }
            return null;
        }
    }
}
