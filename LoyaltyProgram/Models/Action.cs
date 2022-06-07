using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Action
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? ReferrerId { get; set; }
        public double? Points { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? ActionDate { get; set; }
        public double? ReferrerPoints { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public int? OrderId { get; set; }
        public int? MembershipId { get; set; }
        public int? RewardId { get; set; }
        public int? MembershipRewardId { get; set; }
        public int? ReferrerRewardId { get; set; }

        public virtual Program? LoyaltyProgram { get; set; }
        public virtual Membership? Membership { get; set; }
        public virtual Reward? Reward { get; set; }
    }
}
