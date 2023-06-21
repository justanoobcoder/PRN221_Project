using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesObject.DAO;
using BussinessObject.Models;

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
        public void AddRelationship(Relationship relationship) => RelationshipDAO.Instance.AddRelationship(relationship);
        public int GetNextRelationshipId()
        {
            return RelationshipDAO.Instance.GetNextRelationshipId();
        }
        public List<int> CheckRelatedUser(int userId)
        {
            return RelationshipDAO.Instance.CheckRelatedUser(userId);
        }
        public Relationship GetRelationship(int relationshipId)
        {
            return RelationshipDAO.Instance.GetRelationship(relationshipId);
        }
        public void Delete(int userId) => RelationshipDAO.Instance.Delete(userId);

        public bool CheckBelongUser(int userId)
        {
            return RelationshipDAO.Instance.CheckBelongUser(userId);
        }
        }
}
