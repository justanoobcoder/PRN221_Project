using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Family
    {
        public Family()
        {
            Users = new HashSet<User>();
        }

        public int FamilyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Information { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
