using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class OrderAmountActionMembershipMapping
    {
        public int OrderId { get; set; }
        public int MembershipId { get; set; }
        public int? ReferrerId { get; set; }
        public double? Points { get; set; }
        public double? ReferrerPoints { get; set; }
        public DateTime? ActionDate { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual Membership Membership { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
