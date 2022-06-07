using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class OrderItemCondition
    {
        public int Id { get; set; }
        public int? ConditionGroupId { get; set; }
        public int? Quantity { get; set; }
        public int? NextQuantity { get; set; }
        public int? TierSequenceNumber { get; set; }
        public double? QuantityGainPoint { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual ConditionGroup? ConditionGroup { get; set; }
    }
}
