using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Bodt
{
    public class RelationshipDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();
        public List<int> GetRelationship(int userId,int relationshipId)
        {
            List<int> listChildren = new List<int>();
            List<Relationship> relationship;

            try
            {
                // Get From Database
                relationship = context.Relationships.Where(od => od.UserId1 == userId && od.RelationshipDetailId == relationshipId).ToList();
                for(int i = 0; i < relationship.Count; i++)
                {
                    listChildren.Add(relationship[i].UserId2);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listChildren;
        }
        public int GetMainUser(int familyId)
        {
            int mainUser; 
            try
            {
                var query = from user in context.Users
                            join rel in context.Relationships on user.UserId equals rel.UserId2 into userRel
                            from ur in userRel.DefaultIfEmpty()
                            where user.FamilyId == familyId && ur == null
                            select user.UserId;
                mainUser = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mainUser;
        }

        public int getPartner(int userId)
        {
            int partner;
            try
            {
                var query= from row in context.Relationships
                           where (row.UserId1 == userId || row.UserId2 == userId) && row.RelationshipDetailId == 3
                           select row.UserId1 == userId ? row.UserId2 : row.UserId1;
                partner = query.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return partner;
        }
        public int GetNextRelationshipId()
        {
            int nextRelationshipId = -1;

            try
            {
                nextRelationshipId = context.Relationships.Max(u => u.RelationshipId) + 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nextRelationshipId;
        }
        public void AddRelationship(Relationship relationship)
        {
            try
            {
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<int> CheckRelatedUser(int userId)
        {
            List<int> relatedUser = new List<int>();
            try
            {
                List<Relationship> relatedUserList = context.Relationships.Where(od => od.UserId1 == userId).ToList();
                foreach (var relationship in relatedUserList)
                {
                    relatedUser.Add(relationship.UserId2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return relatedUser;
        }
        public Relationship GetRelationship(int relationshipId)
        {
            Relationship relationship = null;
            try
            {
                relationship = context.Relationships.FirstOrDefault(context => context.RelationshipId == relationshipId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return relationship;
        }
        public void Delete(int userId)
        {
            var _context = new FamilyTreeContext();
            try
            {
                Relationship relationshipToRemove = _context.Relationships
                    .FirstOrDefault(od => od.UserId2 == userId);
                _context.Relationships.Remove(relationshipToRemove);
                List<UserJoin> UserJoinToRemove = _context.UserJoins
                    .Where(od => od.UserId == userId)
                    .ToList();
                if (UserJoinToRemove.Count > 0)
                foreach(var userJoin in UserJoinToRemove){
                        _context.UserJoins.Remove(userJoin);
                }
                UserDAO userDAO = new UserDAO();
                _context.Users.Remove(userDAO.GetUser(userId));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckBelongUser(int userId)
        {
            try
            {
                // Check if any relationship exists where UserId1 matches the given userId
                bool hasRelationship = context.Relationships.Any(od => od.UserId1 == userId);
                return hasRelationship;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
