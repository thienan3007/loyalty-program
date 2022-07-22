using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface OrderAmountConditionService
    {
        public PagedList<OrderAmountCondition> GetOrderAmountConditions(PagingParameters pagingParameters);
        public OrderAmountCondition GetOrderAmountCondition(int id);
        public bool Add(OrderAmountCondition orderAmountCondition );
        public bool Update(OrderAmountCondition orderAmountCondition, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
