using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Reward
    {
        public Reward()
        {
            ActionMembershipRewards = new HashSet<Action>();
            ActionReferrerRewards = new HashSet<Action>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Type { get; set; }
        public string? Parameters { get; set; }
        public int? Stock { get; set; }
        public int? Redeemed { get; set; }
        public string? Images { get; set; }
        public int? Status { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public string? Description { get; set; }

        public virtual Program? LoyaltyProgram { get; set; }
        public virtual ICollection<Action>? ActionMembershipRewards { get; set; }
        public virtual ICollection<Action>? ActionReferrerRewards { get; set; }
    }
}
