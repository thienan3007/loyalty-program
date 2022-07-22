using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class VoucherWallet
    {
        public int MembershipId { get; set; }
        public int VoucherDefinitionId { get; set; }
        public int? Status { get; set; }
        public DateTime? UseDate { get; set; }
        public bool? IsPartialRedeemable { get; set; }
        public int? RedeemedValue { get; set; }
        public int? RemainingValue { get; set; }
        public string? Description { get; set; }

        public virtual Membership? Membership { get; set; } = null!;
        public virtual VoucherDefinition? VoucherDefinition { get; set; } = null!;
    }
}
