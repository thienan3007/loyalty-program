using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface RewardService
    {
        public PagedList<Reward> GetRewards(PagingParameters pagingParameters);
        public Reward GetReward(int id);
        public bool AddReward(Reward reward);
        public bool UpdateReward(Reward reward, int id);
        public bool DeleteReward(int id);
        public int GetCount();
    }
}
