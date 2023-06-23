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
        RelationshipDAO relationshipDAO= new RelationshipDAO();
        public List<int> GetRelationship(int userId, int relationshipId)
        {
            return relationshipDAO.GetRelationship(userId, relationshipId);
        }

        public int GetMainUser(int familyId)
        {
            return relationshipDAO.GetMainUser(familyId);
        }
        public int getPartner(int userId)
        {
            return relationshipDAO.getPartner(userId);
        }
        public void AddRelationship(Relationship relationship) => relationshipDAO.AddRelationship(relationship);
        public int GetNextRelationshipId()
        {
            return relationshipDAO.GetNextRelationshipId();
        }
        public List<int> CheckRelatedUser(int userId)
        {
            return relationshipDAO.CheckRelatedUser(userId);
        }
        public Relationship GetRelationship(int relationshipId)
        {
            return relationshipDAO.GetRelationship(relationshipId);
        }
        public void Delete(int userId) => relationshipDAO.Delete(userId);

        public bool CheckBelongUser(int userId)
        {
            return relationshipDAO.CheckBelongUser(userId);
        }
        }
}
