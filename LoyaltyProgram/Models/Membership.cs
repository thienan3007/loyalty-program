using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LoyaltyProgram.Models
{
    public partial class Membership
    {
        public Membership()
        {
            Actions = new HashSet<Action>();
            Cards = new HashSet<Card>();
            Devices = new HashSet<Device>();
            MemberTiers = new HashSet<MemberTier>();
            MembershipCurrencies = new HashSet<MembershipCurrency>();
            Transactions = new HashSet<Transaction>();
            VoucherWallets = new HashSet<VoucherWallet>();
        }

        public int AccountId { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public bool? CanReceivePromotions { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public string? MembershipCode { get; set; }
        public int? ReferrerMemberId { get; set; }
        public DateTime? ReferrerMemberDate { get; set; }
        public int? Status { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? Description { get; set; }

        public virtual Program? LoyaltyProgram { get; set; }
        public virtual ICollection<Action>? Actions { get; set; }
        public virtual ICollection<Card>? Cards { get; set; }
        public virtual ICollection<Device>? Devices { get; set; }
        public virtual ICollection<MemberTier>? MemberTiers { get; set; }
        public virtual ICollection<MembershipCurrency>? MembershipCurrencies { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<VoucherWallet>? VoucherWallets { get; set; }
    }
}
