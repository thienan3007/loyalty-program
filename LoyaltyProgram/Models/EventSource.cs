using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class EventSource
    {
        public int PartnerId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }
    }
}
