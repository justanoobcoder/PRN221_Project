using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class Family
    {
        public Family()
        {
            Users = new HashSet<User>();
        }

        public int FamilyId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Information { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
