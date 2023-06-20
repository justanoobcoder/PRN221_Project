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
        private static RelationshipDAO instance = null;
        private static object instanceLook = new object();

        public static RelationshipDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new RelationshipDAO();
                    }
                    return instance;
                }
            }
        }
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
                            where user.FamilyId == 1 && ur == null
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
            try
            {
                List<int> relationshipOfUser = GetRelationshipOfUser(userId);
                List<int> relatedUser = new List<int>();
                if (relationshipOfUser.Count > 0)
                {
                    foreach (var relationship in relationshipOfUser)
                    {
                        int user = GetRelationship(relationship).UserId2;
                        if (!relatedUser.Contains(user))
                        {
                            relatedUser.Add(user);
                        }
                        context.Relationships.Remove(GetRelationship(relationship));
                    }
                    foreach (var user in relatedUser)
                    {
                        context.Users.Remove(UserDAO.Instance.GetUser(user));
                    }
                }
                    List<Relationship> relationshipsToRemove = context.Relationships
                        .Where(od => od.UserId1 == userId || od.UserId2 == userId)
                        .ToList();
                    foreach (Relationship relationshipToRemove in relationshipsToRemove)
                    {
                        context.Relationships.Remove(relationshipToRemove);
                    }
                    context.Users.Remove(UserDAO.Instance.GetUser(userId));
                    context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<int> GetRelationshipOfUser(int userId)
        {
            List<int> relationshipIdList = new List<int>();
            try
            {
                 List<Relationship> relationshipList = context.Relationships.Where(od => od.UserId1 == userId).ToList();
                 foreach(var relationship in relationshipList)
                 {
                    relationshipIdList.Add(relationship.RelationshipId);
                 }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return relationshipIdList;
        }
    }
}
