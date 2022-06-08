using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class ConditionGroupServiceImpl : ConditionGroupService
    {
        private readonly DatabaseContext _databaseContext;
        public ConditionGroupServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(ConditionGroup conditionGroup)
        {

            _databaseContext.ConditionGroups.Add(conditionGroup);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var conditionGroup = _databaseContext.ConditionGroups.FirstOrDefault(b => b.Id == id);
            if (conditionGroup != null)
            {
                if (conditionGroup.Status == 1)
                {
                    conditionGroup.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public ConditionGroup GetConditionGroup(int id)
        {
            var conditionGroup = _databaseContext.ConditionGroups.FirstOrDefault(b => b.Id == id);
            if (conditionGroup != null)
            {
                if (conditionGroup.Status == 1)
                {
                    return conditionGroup;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.ConditionGroups.Where(b => b.Status == 1).Count();
        }

        public List<ConditionGroup> GetConditionGroups()
        {
            return _databaseContext.ConditionGroups.Where(b => b.Status == 1).ToList();
        }

        public bool Update(ConditionGroup conditionGroup, int id)
        {
            if (conditionGroup != null)
            {
                var conditionGroupDb = GetConditionGroup(id);
                if (conditionGroup != null)
                {
                    if (conditionGroupDb != null)
                    {
                        if (conditionGroupDb.Status == 1)
                        {
                            if (conditionGroup.ConditionRuleId != null)
                                conditionGroupDb.ConditionRuleId = conditionGroup.ConditionRuleId;
                            if (conditionGroup.CreatedDate != null)
                                conditionGroupDb.CreatedDate = conditionGroup.CreatedDate;
                            if (conditionGroup.UpdateDate != null)
                                conditionGroupDb.UpdateDate = conditionGroup.UpdateDate;
                            if (conditionGroup.Status != null)
                                conditionGroupDb.Status = conditionGroup.Status;
                            if (conditionGroup.Description != null)
                                conditionGroupDb.Description = conditionGroup.Description;

                            return _databaseContext.SaveChanges() > 0;
                        }
                    }
                }
            }

            return false;
        }
    }
}
