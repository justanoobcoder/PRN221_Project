using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class UserJoin
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public int View { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
