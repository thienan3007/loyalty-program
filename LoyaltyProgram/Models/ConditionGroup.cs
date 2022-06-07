using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class ConditionGroup
    {
        public ConditionGroup()
        {
            OrderAmountConditions = new HashSet<OrderAmountCondition>();
            OrderItemConditions = new HashSet<OrderItemCondition>();
        }

        public int Id { get; set; }
        public int? ConditionRuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual ConditionRule? ConditionRule { get; set; }
        public virtual ICollection<OrderAmountCondition> OrderAmountConditions { get; set; }
        public virtual ICollection<OrderItemCondition> OrderItemConditions { get; set; }
    }
}
