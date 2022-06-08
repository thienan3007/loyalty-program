using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class OrderAmountConditionServiceImpl : OrderAmountConditionService
    {
        private readonly DatabaseContext _databaseContext;
        public OrderAmountConditionServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add( OrderAmountCondition orderAmountCondition)
        {

            _databaseContext.OrderAmountConditions.Add(orderAmountCondition);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var orderAmountCondition = _databaseContext.OrderAmountConditions.FirstOrDefault(b => b.Id == id);
            if (orderAmountCondition != null)
            {
                if (orderAmountCondition.Status == 1)
                {
                    orderAmountCondition.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public OrderAmountCondition GetOrderAmountCondition(int id)
        {
            var orderAmountCondition = _databaseContext.OrderAmountConditions.FirstOrDefault(b => b.Id == id);
            if (orderAmountCondition != null)
            {
                if (orderAmountCondition.Status == 1)
                {
                    return orderAmountCondition;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.OrderAmountConditions.Where(b => b.Status == 1).Count();
        }

        public List<OrderAmountCondition> GetOrderAmountConditions()
        {
            return _databaseContext.OrderAmountConditions.Where(b => b.Status == 1).ToList();
        }

        public bool Update(OrderAmountCondition orderAmountCondition, int id)
        {
            if (orderAmountCondition != null)
            {
                var orderAmountConditionDb = GetOrderAmountCondition(id);
                if (orderAmountConditionDb != null)
                {
                    if (orderAmountConditionDb.Status == 1)
                    {
                        if (orderAmountCondition.ConditionGroupId != null)
                            orderAmountConditionDb.ConditionGroupId = orderAmountCondition.ConditionGroupId;
                        if (orderAmountCondition.MinOrderAmount != null)
                            orderAmountConditionDb.MinOrderAmount = orderAmountCondition.MinOrderAmount;
                        if (orderAmountCondition.NextOrderTotalAmount != null)
                            orderAmountConditionDb.NextOrderTotalAmount = orderAmountCondition.NextOrderTotalAmount;
                        if (orderAmountCondition.OrderTotalAmountGainPoint != null)
                            orderAmountConditionDb.OrderTotalAmountGainPoint = orderAmountCondition.OrderTotalAmountGainPoint;
                        if (orderAmountCondition.OrderTotalAmountAfterDiscount != null)
                            orderAmountConditionDb.OrderTotalAmountAfterDiscount = orderAmountCondition.OrderTotalAmountAfterDiscount;
                        if (orderAmountCondition.NextOrderTotalAmountAfterDiscount != null)
                            orderAmountConditionDb.NextOrderTotalAmountAfterDiscount = orderAmountCondition.NextOrderTotalAmountAfterDiscount;
                        if (orderAmountCondition.OrderTotalAmountAfterDiscountGainPoint != null)
                            orderAmountConditionDb.OrderTotalAmountAfterDiscountGainPoint = orderAmountCondition.OrderTotalAmountAfterDiscountGainPoint;
                        if (orderAmountCondition.TierSequenceNumber != null)
                            orderAmountConditionDb.TierSequenceNumber = orderAmountCondition.TierSequenceNumber;
                        if (orderAmountCondition.Status != null)
                            orderAmountConditionDb.Status = orderAmountCondition.Status;
                        if (orderAmountCondition.Description != null)
                            orderAmountConditionDb.Description = orderAmountCondition.Description;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
