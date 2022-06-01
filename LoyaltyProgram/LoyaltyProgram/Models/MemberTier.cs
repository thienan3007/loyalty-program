using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class MemberTier
    {
        public int LoyaltyMemberId { get; set; }
        public int LoyaltyTierId { get; set; }
        public string? Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? UdpateTierDate { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual Membership LoyaltyMember { get; set; } = null!;
        public virtual Tier LoyaltyTier { get; set; } = null!;
    }
}
