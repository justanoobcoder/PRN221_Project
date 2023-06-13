using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
