using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class UserJoin
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int View { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
