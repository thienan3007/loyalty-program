using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? MembershipId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? Points { get; set; }
        public int? ReferrerId { get; set; }
        public double? ReferrerPoints { get; set; }
        public bool? Status { get; set; }
        public int? OrderId { get; set; }
        public string? Description { get; set; }
        public int? MemberCurrencyId { get; set; }

        public virtual MembershipCurrency? MemberCurrency { get; set; }
    }
}
