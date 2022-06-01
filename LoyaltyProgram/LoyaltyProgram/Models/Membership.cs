using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Membership
    {
        public Membership()
        {
            MemberTiers = new HashSet<MemberTier>();
            MembershipCurrencies = new HashSet<MembershipCurrency>();
            OrderAmountActionMembershipMappings = new HashSet<OrderAmountActionMembershipMapping>();
            OrderItemActionMembershipMappings = new HashSet<OrderItemActionMembershipMapping>();
        }

        public int AccountId { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public DateTime? EnrollmenDate { get; set; }
        public bool? CanReceivePromotions { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public string? MembershipCode { get; set; }
        public int? ReferrerMemberId { get; set; }
        public DateTime? ReferrerMemberDate { get; set; }

        public virtual LoyaltyProgram? LoyaltyProgram { get; set; }
        public virtual ICollection<MemberTier> MemberTiers { get; set; }
        public virtual ICollection<MembershipCurrency> MembershipCurrencies { get; set; }
        public virtual ICollection<OrderAmountActionMembershipMapping> OrderAmountActionMembershipMappings { get; set; }
        public virtual ICollection<OrderItemActionMembershipMapping> OrderItemActionMembershipMappings { get; set; }
    }
}
