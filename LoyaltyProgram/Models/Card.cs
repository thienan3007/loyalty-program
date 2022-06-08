using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Card
    {
        public Card()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public double? Discount { get; set; }
        public string? CardholderName { get; set; }
        public int? MembershipId { get; set; }
        public int? BrandId { get; set; }
        public int? Status { get; set; }
        public int? CurrencyId { get; set; }
        public double? Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual Membership? Membership { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
