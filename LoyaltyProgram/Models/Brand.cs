using System;
using System.Collections.Generic;

namespace LoyaltyProgram.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Programs = new HashSet<Program>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? OrganizationId { get; set; }
        public int? Status { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
