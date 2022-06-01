using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class ConditionRule
    {
        public ConditionRule()
        {
            ConditionGroups = new HashSet<ConditionGroup>();
        }

        public int Id { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? MaxPoints { get; set; }
        public double? SpendingValue { get; set; }
        public double? MinPointsForRedemption { get; set; }
        public double? MinRedeemablePoints { get; set; }
        public double? MinRedeemableAmount { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual LoyaltyProgram? LoyaltyProgram { get; set; }
        public virtual ICollection<ConditionGroup> ConditionGroups { get; set; }
    }
}
