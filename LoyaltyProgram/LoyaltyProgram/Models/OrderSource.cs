using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class OrderSource
    {
        public OrderSource()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? PartnerId { get; set; }
        public string? Address { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual LoyaltyProgram? LoyaltyProgram { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
