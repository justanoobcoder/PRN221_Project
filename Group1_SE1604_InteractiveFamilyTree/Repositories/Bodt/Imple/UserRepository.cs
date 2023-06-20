using Repositories.Bodt.Imple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.Models;
using DataAcessObject.Bodt;

namespace Repositories.Bodt
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user) => UserDAO.Instance.AddUser(user);

        public void DeleteUser(int userId) => UserDAO.Instance.Delete(userId);


        public User GetUser(int userId)
        {
            return UserDAO.Instance.GetUser(userId);
        }

        public User GetUserByEmail(string email)
        {
            return UserDAO.Instance.GetUserByEmail(email);
        }

        public List<User> GetUserList()
        {
            return UserDAO.Instance.GetUserList();
        }
        public List<User> GetUserListByFamilyId(int familyId)
        {
            return UserDAO.Instance.GetUserListByFamilyId(familyId);
        }

        public User Login(string email, string password)
        {
            return UserDAO.Instance.Login(email, password);
        }

        public void Update(User user) => UserDAO.Instance.Update(user);

        public List<User> searchUser(string search)
        {
            return UserDAO.Instance.searchUser(search);
        }
        public int familyCount(int familyId)
        {
            return UserDAO.Instance.familyCount(familyId);  
        }
        public List<User> getMarriedUser(int FamilyId)
        { return UserDAO.Instance.getMarriedUser(FamilyId); }
        public List<User> getUnavailable(int FamilyId)
        {
            return UserDAO.Instance.getUnavailable(FamilyId);
        }
    }
}
