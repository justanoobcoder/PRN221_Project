using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class Event
    {
        public Event()
        {
            UserJoins = new HashSet<UserJoin>();
        }

        public int EventId { get; set; }
        public int? CreatorId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Information { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<UserJoin> UserJoins { get; set; }
    }
}
