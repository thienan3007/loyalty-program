using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class OrderAmountCondition
    {
        public int Id { get; set; }
        public int? ConditionGroupId { get; set; }
        public double? OrderTotalAmount { get; set; }
        public double? MinOrderAmount { get; set; }
        public double? NextOrderTotalAmount { get; set; }
        public double? OrderTotalAmountGainPoint { get; set; }
        public double? OrderTotalAmountAfterDiscount { get; set; }
        public double? NextOrderTotalAmountAfterDiscount { get; set; }
        public double? OrderTotalAmountAfterDiscountGainPoint { get; set; }
        public int? TierSequenceNumber { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual ConditionGroup? ConditionGroup { get; set; }
    }
}
