using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class OrderAmountCondition
    {
        public int Id { get; set; }
        public int? ConditionGroupId { get; set; }
        public int? MinOrderAmount { get; set; }
        public int? NextOrderTotalAmount { get; set; }
        public int? OrderTotalAmountGainPoint { get; set; }
        public int? OrderTotalAmountAfterDiscount { get; set; }
        public int? NextOrderTotalAmountAfterDiscont { get; set; }
        public int? OrderTotalAmountAfterDiscountGainPoint { get; set; }
        public int? TierSequenceNumber { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual ConditionGroup? ConditionGroup { get; set; }
    }
}
