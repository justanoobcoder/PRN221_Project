using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class RelationshipDetail
    {
        public RelationshipDetail()
        {
            Relationships = new HashSet<Relationship>();
        }

        public int RelationshipDetailId { get; set; }
        public string RelationshipName { get; set; } = null!;

        public virtual ICollection<Relationship> Relationships { get; set; }
    }
}
