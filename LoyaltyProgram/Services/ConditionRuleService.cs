using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface ConditionRuleService
    {
        public PagedList<ConditionRule> GetConditionRules(PagingParameters pagingParameters);
        public List<ConditionRule> FindAll();
        public ConditionRule GetConditionRule(int id);
        public bool Add(ConditionRule conditionRule);
        public bool Update(ConditionRule conditionRule, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
