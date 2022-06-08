using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class ConditionRuleServiceImpl : ConditionRuleService
    {
        private readonly DatabaseContext _databaseContext;
        public ConditionRuleServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(ConditionRule conditionRule)
        {

            _databaseContext.ConditionRules.Add(conditionRule);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var conditionRule= _databaseContext.ConditionRules.FirstOrDefault(b => b.Id == id);
            if (conditionRule != null)
            {
                if (conditionRule.Status == 1)
                {
                    conditionRule.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public ConditionRule GetConditionRule(int id)
        {
            var conditionRule = _databaseContext.ConditionRules.FirstOrDefault(b => b.Id == id);
            if (conditionRule != null)
            {
                if (conditionRule.Status == 1)
                {
                    return conditionRule;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.ConditionRules.Where(b => b.Status == 1).Count();
        }

        public List<ConditionRule> GetConditionRules()
        {
            return _databaseContext.ConditionRules.Where(b => b.Status == 1).ToList();
        }

        public bool Update(ConditionRule conditionRule, int id)
        {
            if (conditionRule != null)
            {
                var conditionRuleDb = GetConditionRule(id);
                if (conditionRuleDb != null)
                {
                    if (conditionRuleDb.Status == 1)
                    {
                        if (conditionRule.LoyaltyProgramId != null)
                            conditionRuleDb.LoyaltyProgramId = conditionRule.LoyaltyProgramId;
                        if (conditionRule.CreatedAt != null)
                            conditionRuleDb.CreatedAt = conditionRule.CreatedAt;
                        if (conditionRule.IsActive != null)
                            conditionRuleDb.IsActive = conditionRule.IsActive;
                        if (conditionRule.StartDate != null)
                            conditionRuleDb.StartDate = conditionRule.StartDate;
                        if (conditionRule.EndDate != null)
                            conditionRuleDb.EndDate = conditionRule.EndDate;
                        if (conditionRule.MaxPoints != null)
                            conditionRuleDb.MaxPoints = conditionRule.MaxPoints;
                        if (conditionRule.SpendingValue != null)
                            conditionRuleDb.SpendingValue = conditionRule.SpendingValue;
                        if (conditionRule.MinPointsForRedemption != null)
                            conditionRuleDb.MinPointsForRedemption = conditionRule.MinPointsForRedemption;
                        if (conditionRule.MinRedeemablePoints != null)
                            conditionRuleDb.MinRedeemablePoints = conditionRule.MinRedeemablePoints;
                        if (conditionRule.Status != null)
                            conditionRuleDb.Status = conditionRule.Status;
                        if (conditionRule.Description != null)
                            conditionRuleDb.Description = conditionRule.Description;
                        if (conditionRule.MinRedeemableAmount != null)
                            conditionRuleDb.MinRedeemableAmount = conditionRule.MinRedeemableAmount;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
