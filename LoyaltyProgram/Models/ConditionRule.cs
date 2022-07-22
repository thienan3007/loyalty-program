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
        public int? MaxPoints { get; set; }
        public int? SpendingValue { get; set; }
        public int? MinPointsForRedemption { get; set; }
        public int? MinRedeemablePoints { get; set; }
        public int? MinRedeemableAmount { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual Program? LoyaltyProgram { get; set; }
        public virtual ICollection<ConditionGroup>? ConditionGroups { get; set; }
    }
}
