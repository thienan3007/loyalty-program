using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface ActionService
    {
        public PagedList<Models.Action> GetActions(PagingParameters pagingParameters);
        public Models.Action GetAction(int id);
        public bool AddAction(Models.Action action);
        public bool UpdateAction(Models.Action action, int id);
        public bool DeleteAction(int id);
        public int GetCount();
    }
}
