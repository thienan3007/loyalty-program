using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderAmountActionMembershipMappings = new HashSet<OrderAmountActionMembershipMapping>();
            OrderItemActionMembershipMappings = new HashSet<OrderItemActionMembershipMapping>();
        }

        public int Id { get; set; }
        public int? OrderSourceId { get; set; }
        public int? MemberShipId { get; set; }
        public double? TotalOrderBonusPoints { get; set; }
        public double? TotalAmount { get; set; }
        public int? RuleTypeId { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }
        public string? OrderJsonString { get; set; }

        public virtual OrderSource? OrderSource { get; set; }
        public virtual ICollection<OrderAmountActionMembershipMapping> OrderAmountActionMembershipMappings { get; set; }
        public virtual ICollection<OrderItemActionMembershipMapping> OrderItemActionMembershipMappings { get; set; }
    }
}
