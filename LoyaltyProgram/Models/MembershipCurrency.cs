using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class MembershipCurrency
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? PointsBalance { get; set; }
        public double? TotalPointsRedeemed { get; set; }
        public double? TotalPointsExpired { get; set; }
        public double? TotalPointsAccrued { get; set; }
        public double? BalanceBeforeReset { get; set; }
        public DateTime? LastResetDate { get; set; }
        public double? ExpirationPoints { get; set; }
        public int? MembershipId { get; set; }
        public int? CurrencyId { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual Membership? Membership { get; set; }
    }
}
