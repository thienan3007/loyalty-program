using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface ConditionGroupService
    {
        public PagedList<ConditionGroup> GetConditionGroups(PagingParameters pagingParameters);
        public List<ConditionGroup> FindAll();
        public ConditionGroup GetConditionGroup(int id);
        public bool Add(ConditionGroup conditionGroup);
        public bool Update(ConditionGroup conditionGroup, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
