using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
