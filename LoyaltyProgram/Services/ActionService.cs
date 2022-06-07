using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface ActionService
    {
        public List<Models.Action> GetActions();
        public Models.Action GetAction(int id);
        public bool AddAction(Models.Action action);
        public bool UpdateAction(Models.Action action, int id);
        public bool DeleteAction(int id);
        public int GetCount();
    }
}
