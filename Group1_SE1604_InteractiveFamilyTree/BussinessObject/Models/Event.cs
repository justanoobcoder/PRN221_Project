using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Event
    {
        public Event()
        {
            UserJoins = new HashSet<UserJoin>();
        }

        public int EventId { get; set; }
        public int? CreatorId { get; set; }
        public string EventName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Information { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserJoin> UserJoins { get; set; }
    }
}
