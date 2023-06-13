using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Relationship
    {
        public int RelationshipId { get; set; }
        public int UserId1 { get; set; }
        public int RelationshipDetailId { get; set; }
        public int UserId2 { get; set; }

        public virtual RelationshipDetail RelationshipDetail { get; set; }
        public virtual User UserId1Navigation { get; set; }
        public virtual User UserId2Navigation { get; set; }
    }
}
