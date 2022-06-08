using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? MembershipId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? Status { get; set; }
        public int? OrderId { get; set; }
        public string? Description { get; set; }
        public int? CardId { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Card? Card { get; set; }
        public virtual Membership? Membership { get; set; }
    }
}
