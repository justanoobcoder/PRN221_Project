using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public interface IRelationshipRepository
    {
        public List<int> GetRelationship(int userId, int relationshipId);
        public int GetMainUser(int familyId);
        public int getPartner(int userId);
    }
}
