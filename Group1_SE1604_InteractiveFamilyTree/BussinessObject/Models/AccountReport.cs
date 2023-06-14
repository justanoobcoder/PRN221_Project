using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class AccountReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? Date { get; set; }
    }
}
