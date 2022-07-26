using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class ConditionGroupServiceImpl : ConditionGroupService

    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<ConditionGroup> _sortHelper;
        public ConditionGroupServiceImpl(DatabaseContext databaseContext, ISortHelper<ConditionGroup> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
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

        public PagedList<ConditionGroup> GetConditionGroups(PagingParameters pagingParameters)
        {
            var conditionGroups = _databaseContext.ConditionGroups.Where(b => b.Status == 1);
            var sortedConditionGroups = _sortHelper.ApplySort(conditionGroups, pagingParameters.OrderBy);

            if (conditionGroups != null)
            {
                return PagedList<ConditionGroup>.ToPagedList(sortedConditionGroups, pagingParameters.PageNumber,
                    pagingParameters.PageSize);
            }
            return null;
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

        public List<ConditionGroup> FindAll()
        {
            var conditionGroupList = _databaseContext.ConditionGroups.ToList();
            if (conditionGroupList != null)
            {
                return conditionGroupList;
            }
            return null;
        }
    }
}
