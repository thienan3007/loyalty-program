using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class VoucherDefinition
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? DiscountValue { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? VoucherCode { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }
        public int? ExpirationPeriod { get; set; }
        public string? ExpirationPeriodUnits { get; set; }
        public bool? IsPartialRedeemable { get; set; }
    }
}
