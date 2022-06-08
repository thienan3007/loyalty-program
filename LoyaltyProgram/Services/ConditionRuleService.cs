using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface ConditionRuleService
    {
        public List<ConditionRule> GetConditionRules();
        public ConditionRule GetConditionRule(int id);
        public bool Add(ConditionRule conditionRule);
        public bool Update(ConditionRule conditionRule, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
