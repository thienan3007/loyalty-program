using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Brands = new HashSet<Brand>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
    }
}
