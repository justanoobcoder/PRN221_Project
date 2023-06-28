using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class EventReport
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DateReported { get; set; }
        public int? ReporterId { get; set; }
        public string Reason { get; set; }

        public virtual Event Event { get; set; }
        public virtual User Reporter { get; set; }
        public virtual Status Status { get; set; }
    }
}
