using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface ConditionGroupService
    {
        public List<ConditionGroup> GetConditionGroups();
        public ConditionGroup GetConditionGroup(int id);
        public bool Add(ConditionGroup conditionGroup);
        public bool Update(ConditionGroup conditionGroup, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
