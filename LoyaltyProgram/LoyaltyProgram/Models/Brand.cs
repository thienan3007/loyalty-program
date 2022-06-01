using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Brand
    {
        public Brand()
        {
            LoyaltyPrograms = new HashSet<LoyaltyProgram>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }
        public int? OrganizationId { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }
    }
}
