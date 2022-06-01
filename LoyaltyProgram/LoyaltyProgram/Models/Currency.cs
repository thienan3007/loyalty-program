using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Currency
    {
        public Currency()
        {
            MembershipCurrencies = new HashSet<MembershipCurrency>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? NextResetDate { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }
        public int? LoyaltyProgramId { get; set; }

        public virtual LoyaltyProgram? LoyaltyProgram { get; set; }
        public virtual ICollection<MembershipCurrency> MembershipCurrencies { get; set; }
    }
}
