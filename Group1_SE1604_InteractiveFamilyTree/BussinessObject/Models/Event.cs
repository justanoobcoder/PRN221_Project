
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Event
    {
        public Event()
        {
            EventReports = new HashSet<EventReport>();
            UserJoins = new HashSet<UserJoin>();
        }

        public int EventId { get; set; }
        public int? CreatorId { get; set; }
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Information is required")]
        public string Information { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<EventReport> EventReports { get; set; }
        public virtual ICollection<UserJoin> UserJoins { get; set; }
    }
}
