using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class OrderItemConditionServiceImpl : OrderItemConditionService
    {
        private readonly DatabaseContext _databaseContext;
        public OrderItemConditionServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(OrderItemCondition orderItemCondition)
        {

            _databaseContext.OrderItemConditions.Add(orderItemCondition);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var orderItemCondition = _databaseContext.OrderItemConditions.FirstOrDefault(b => b.Id == id);
            if (orderItemCondition != null)
            {
                if (orderItemCondition.Status == 1)
                {
                    orderItemCondition.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public OrderItemCondition GetOrderItemCondition(int id)
        {
            var orderItemCondition = _databaseContext.OrderItemConditions.FirstOrDefault(b => b.Id == id);
            if (orderItemCondition != null)
            {
                if (orderItemCondition.Status == 1)
                {
                    return orderItemCondition;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.OrderItemConditions.Where(b => b.Status == 1).Count();
        }

        public List<OrderItemCondition> GetOrderItemConditions()
        {
            return _databaseContext.OrderItemConditions.Where(b => b.Status == 1).ToList();
        }

        public bool Update(OrderItemCondition orderItemCondition, int id)
        {
            var orderItemConditionDb = GetOrderItemCondition(id);
            if (orderItemCondition != null)
            {
                if (orderItemConditionDb != null)
                {
                    if (orderItemConditionDb.Status == 1)
                    {
                        if (orderItemCondition.ConditionGroupId != null)
                            orderItemConditionDb.ConditionGroupId = orderItemCondition.ConditionGroupId;
                        if (orderItemCondition.Quantity != null)
                            orderItemConditionDb.Quantity = orderItemCondition.Quantity;
                        if (orderItemCondition.NextQuantity != null)
                            orderItemConditionDb.NextQuantity = orderItemCondition.NextQuantity;
                        if (orderItemCondition.TierSequenceNumber != null)
                            orderItemConditionDb.TierSequenceNumber = orderItemCondition.TierSequenceNumber;
                        if (orderItemCondition.QuantityGainPoint != null)
                            orderItemConditionDb.QuantityGainPoint = orderItemCondition.QuantityGainPoint;
                        if (orderItemCondition.Status != null)
                            orderItemConditionDb.Status = orderItemCondition.Status;
                        if (orderItemCondition.Description != null)
                            orderItemConditionDb.Description = orderItemCondition.Description;
                        if (orderItemCondition.ProductId != null)
                            orderItemConditionDb.ProductId = orderItemCondition.ProductId;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
