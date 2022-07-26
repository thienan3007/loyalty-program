using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class RewardServiceImpl : RewardService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<Reward> _sortHelper;
        public RewardServiceImpl(DatabaseContext databaseContext, ISortHelper<Reward> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
        }

        public bool AddReward(Reward reward)
        {

            _databaseContext.Rewards.Add(reward);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteReward(int id)
        {
            var reward = _databaseContext.Rewards.FirstOrDefault(b => b.Id == id);
            if (reward != null)
            {
                if (reward.Status == 1)
                {
                    reward.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Reward GetReward(int id)
        {
            var reward = _databaseContext.Rewards.FirstOrDefault(b => b.Id == id);
            if (reward != null)
            {
                if (reward.Status == 1)
                {
                    return reward;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Rewards.Where(b => b.Status == 1).Count();
        }

        public PagedList<Reward> GetRewards(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Reward> rewards;
            if (filterString != null)
            {
                rewards = _databaseContext.Rewards.Where(b => b.Status == 1 && b.Name.Contains(filterString));
            }
            else
            {
                rewards = _databaseContext.Rewards.Where(b => b.Status == 1);
            }

            var sortedRewards = _sortHelper.ApplySort(rewards, pagingParameters.OrderBy);
            if (rewards != null)
            {
                return PagedList<Reward>.ToPagedList(sortedRewards, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateReward(Reward reward, int id)
        {
            if (reward != null)
            {
                var rewardDb = GetReward(id);
                if (rewardDb != null)
                {
                    if (rewardDb.Status == 1)
                    {
                        if (reward.Name != null)
                            rewardDb.Name = reward.Name;
                        if (reward.CreatedAt != null)
                            rewardDb.CreatedAt = reward.CreatedAt;
                        if (reward.Type != null)
                            reward.Type = reward.Type;
                        if (reward.Parameters != null)
                            rewardDb.Parameters = reward.Parameters;
                        if (reward.Stock != null)
                            rewardDb.Stock = reward.Stock;
                        if (reward.Redeemed != null)
                            rewardDb.Redeemed = reward.Redeemed;
                        if (reward.Images != null)
                            rewardDb.Images = reward.Images;
                        if (reward.Status != null)
                            rewardDb.Status = reward.Status;
                        if (reward.Description != null)
                            rewardDb.Description = reward.Description;
                        if (reward.LoyaltyProgramId != null)
                            rewardDb.LoyaltyProgramId = reward.LoyaltyProgramId;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
