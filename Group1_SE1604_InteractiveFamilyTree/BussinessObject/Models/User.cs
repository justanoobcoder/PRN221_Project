
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BussinessObject.Models
{
    public partial class User
    {
        public User()
        {
            AccountReports = new HashSet<AccountReport>();
            EventReports = new HashSet<EventReport>();
            Events = new HashSet<Event>();
            RelationshipUserId1Navigations = new HashSet<Relationship>();
            RelationshipUserId2Navigations = new HashSet<Relationship>();
            UserJoins = new HashSet<UserJoin>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public int? FamilyId { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Img { get; set; }

        public virtual Family Family { get; set; }
        public virtual ICollection<AccountReport> AccountReports { get; set; }
        public virtual ICollection<EventReport> EventReports { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Relationship> RelationshipUserId1Navigations { get; set; }
        public virtual ICollection<Relationship> RelationshipUserId2Navigations { get; set; }
        public virtual ICollection<UserJoin> UserJoins { get; set; }

        [NotMapped]
        public virtual int PartnerId { get; set; }
        [NotMapped]
        public virtual List<User> Children { get; set; }
    }
}
 