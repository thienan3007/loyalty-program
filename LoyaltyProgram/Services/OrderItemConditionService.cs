using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface OrderItemConditionService
    {
        public List<OrderItemCondition> GetOrderItemConditions();
        public OrderItemCondition GetOrderItemCondition(int id);
        public bool Add(OrderItemCondition orderItemCondition);
        public bool Update(OrderItemCondition orderItemCondition, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
