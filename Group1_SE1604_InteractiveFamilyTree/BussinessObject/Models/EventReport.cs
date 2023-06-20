using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class EventReport
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int? Status { get; set; }
    }
}
