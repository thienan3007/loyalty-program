using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Program
    {
        public Program()
        {
            Actions = new HashSet<Action>();
            ConditionRules = new HashSet<ConditionRule>();
            Memberships = new HashSet<Membership>();
            Rewards = new HashSet<Reward>();
            Tiers = new HashSet<Tier>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? BrandId { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual ICollection<Action>? Actions { get; set; }
        public virtual ICollection<ConditionRule>? ConditionRules { get; set; }
        public virtual ICollection<Membership>? Memberships { get; set; }
        public virtual ICollection<Reward>? Rewards { get; set; }
        public virtual ICollection<Tier>? Tiers { get; set; }
    }
}
