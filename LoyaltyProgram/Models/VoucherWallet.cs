﻿using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class VoucherWallet
    {
        public int MembershipId { get; set; }
        public int VoucherDefinitionId { get; set; }
        public bool? Status { get; set; }
        public DateTime? UseDate { get; set; }
        public bool? IsPartialRedeemable { get; set; }
        public double? RedeemedValue { get; set; }
        public double? RemainingValue { get; set; }
        public string? Description { get; set; }
    }
}