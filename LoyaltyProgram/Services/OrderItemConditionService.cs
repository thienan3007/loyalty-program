using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface OrderItemConditionService
    {
        public PagedList<OrderItemCondition> GetOrderItemConditions(PagingParameters pagingParameters);
        public OrderItemCondition GetOrderItemCondition(int id);
        public bool Add(OrderItemCondition orderItemCondition);
        public bool Update(OrderItemCondition orderItemCondition, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
