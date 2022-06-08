using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface RewardService
    {
        public List<Reward> GetRewards();
        public Reward GetReward(int id);
        public bool AddReward(Reward reward);
        public bool UpdateReward(Reward reward, int id);
        public bool DeleteReward(int id);
        public int GetCount();
    }
}
