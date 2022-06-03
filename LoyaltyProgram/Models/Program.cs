using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Program
    {
        public Program()
        {
            ConditionRules = new HashSet<ConditionRule>();
            Currencies = new HashSet<Currency>();
            Memberships = new HashSet<Membership>();
            OrderSources = new HashSet<OrderSource>();
            Tiers = new HashSet<Tier>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? BrandId { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual ICollection<ConditionRule> ConditionRules { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<OrderSource> OrderSources { get; set; }
        public virtual ICollection<Tier> Tiers { get; set; }
    }
}
