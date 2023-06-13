using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class AccountReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? Status { get; set; }
    }
}
