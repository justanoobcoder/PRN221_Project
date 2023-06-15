using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesObject.DAO;
namespace Repositories.Bodt.Imple
{
    public class RelationshipRepository : IRelationshipRepository
    {
        public List<int> GetRelationship(int userId, int relationshipId)
        {
            return RelationshipDAO.Instance.GetRelationship(userId, relationshipId);
        }

        public int GetMainUser(int familyId)
        {
            return RelationshipDAO.Instance.GetMainUser(familyId);
        }
        public int getPartner(int userId)
        {
            return RelationshipDAO.Instance.getPartner(userId);
        }
    }
}
