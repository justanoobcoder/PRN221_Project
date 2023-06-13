using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class RelationshipDetail
    {
        public RelationshipDetail()
        {
            Relationships = new HashSet<Relationship>();
        }

        public int RelationshipDetailId { get; set; }
        public string RelationshipName { get; set; }

        public virtual ICollection<Relationship> Relationships { get; set; }
    }
}
