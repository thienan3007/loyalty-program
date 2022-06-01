using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class MemberReferrerLevel
    {
        public int Id { get; set; }
        public int? TierSequenceNumber { get; set; }
        public double? RatioReferrerPoints { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }
    }
}
