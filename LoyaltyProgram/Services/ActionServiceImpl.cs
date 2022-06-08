using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class ActionServiceImpl : ActionService
    {
        private readonly DatabaseContext _databaseContext;
        public ActionServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddAction(Models.Action action)
        {

            _databaseContext.Actions.Add(action);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteAction(int id)
        {
            var action = _databaseContext.Actions.FirstOrDefault(b => b.Id == id);
            if (action != null)
            {
                if (action.Status == 1)
                {
                    action.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Models.Action GetAction(int id)
        {
            var action = _databaseContext.Actions.FirstOrDefault(b => b.Id == id);
            if (action != null)
            {
                if (action.Status == 1)
                {
                    return action;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Actions.Where(b => b.Status == 1).Count();
        }

        public List<Models.Action> GetActions()
        {
            return _databaseContext.Actions.Where(b => b.Status == 1).ToList();
        }

        public bool UpdateAction(Models.Action action, int id)
        {
            if (action != null)
            {
                var actionDb = GetAction(id);
                if (actionDb != null)
                {
                    if (actionDb.Status == 1)
                    {
                        if (action.Type != null)
                            actionDb.Type = action.Type;
                        if (action.ReferrerId != null)
                            actionDb.ReferrerId = action.ReferrerId;
                        if (action.Points != null)
                            actionDb.Points = action.Points;
                        if (action.Status != null)
                            actionDb.Status = action.Status;
                        if (action.Description != null)
                            actionDb.Description = action.Description;
                        if (action.ActionDate != null)
                            actionDb.ActionDate = action.ActionDate;
                        if (action.ReferrerPoints != null)
                            actionDb.ReferrerPoints = action.ReferrerPoints;
                        if (action.LoyaltyProgramId != null)
                            actionDb.LoyaltyProgramId = action.LoyaltyProgramId;
                        if (action.OrderId != null)
                            actionDb.OrderId = action.OrderId;
                        if (action.MembershipId != null)
                            actionDb.MembershipId = action.MembershipId;
                        if (action.MembershipRewardId != null)
                            actionDb.MembershipRewardId = action.MembershipRewardId;
                        if (action.ReferrerRewardId != null)
                            actionDb.ReferrerRewardId = action.ReferrerRewardId;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
