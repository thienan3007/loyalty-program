using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Noti
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsRead { get; set; }
    }
}
