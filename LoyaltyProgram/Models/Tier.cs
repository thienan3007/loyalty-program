using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Tier
    {
        public Tier()
        {
            MemberTiers = new HashSet<MemberTier>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public int? SequenceNumber { get; set; }
        public double? MinPoints { get; set; }
        public double? RatioPoints { get; set; }
        public string? Description { get; set; }

        public virtual Program? LoyaltyProgram { get; set; }
        public virtual ICollection<MemberTier> MemberTiers { get; set; }
    }
}
