using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Device
    {
        public int AccountId { get; set; }
        public string DeviceId { get; set; } = null!;

        public virtual Membership Account { get; set; } = null!;
    }
}
