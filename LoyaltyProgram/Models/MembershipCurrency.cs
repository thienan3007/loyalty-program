using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class MembershipCurrency
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? PointsBalance { get; set; }
        public int? TotalPointsRedeemed { get; set; }
        public int? TotalPointsExpired { get; set; }
        public int? TotalPointsAccrued { get; set; }
        public int? BalanceBeforeReset { get; set; }
        public DateTime? LastResetDate { get; set; }
        public int? ExpirationPoints { get; set; }
        public int MembershipId { get; set; }
        public int? CurrencyId { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual Membership? Membership { get; set; }
    }
}
