using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class EventReport
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int? Status { get; set; }
    }
}
