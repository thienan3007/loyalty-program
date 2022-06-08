using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface OrderAmountConditionService
    {
        public List<OrderAmountCondition> GetOrderAmountConditions();
        public OrderAmountCondition GetOrderAmountCondition(int id);
        public bool Add(OrderAmountCondition orderAmountCondition );
        public bool Update(OrderAmountCondition orderAmountCondition, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
