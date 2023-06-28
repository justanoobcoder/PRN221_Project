using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Status
    {
        public Status()
        {
            AccountReports = new HashSet<AccountReport>();
            EventReports = new HashSet<EventReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AccountReport> AccountReports { get; set; }
        public virtual ICollection<EventReport> EventReports { get; set; }
    }
}
