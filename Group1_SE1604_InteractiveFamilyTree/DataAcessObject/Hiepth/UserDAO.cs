using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Hiepth
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static object instanceLook = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        FamilyTreeContext context = new FamilyTreeContext();

        public List<User> GetUsersHaveAccountByFamilyId(int familyId)
        {
            try
            {
                return context.Users
                    .Where(u => u.FamilyId == familyId && u.Password != null)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetById(int id)
        {
            try
            {
                return context.Users
                    .Where(u => u.UserId == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
