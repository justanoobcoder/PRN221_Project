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
        UserDAO userDAO= new UserDAO();
        public void AddUser(User user) => userDAO.AddUser(user);

        public void DeleteUser(int userId) => userDAO.Delete(userId);


        public User GetUser(int userId)
        {
            return userDAO.GetUser(userId);
        }

        public User GetUserByEmail(string email)
        {
            return userDAO.GetUserByEmail(email);
        }

        public List<User> GetUserList()
        {
            return userDAO.GetUserList();
        }
        public List<User> GetUserListByFamilyId(int familyId)
        {
            return userDAO.GetUserListByFamilyId(familyId);
        }

        public User Login(string email, string password)
        {
            return userDAO.Login(email, password);
        }

        public void Update(User user) => userDAO.Update(user);

        public List<User> searchUser(string search)
        {
            return userDAO.searchUser(search);
        }
        public int familyCount(int familyId)
        {
            return userDAO.familyCount(familyId);  
        }
        public List<User> getMarriedUser(int FamilyId)
        { return userDAO.getMarriedUser(FamilyId); }
        public List<User> getUnavailable(int FamilyId)
        {
            return userDAO.getUnavailable(FamilyId);
        }
        public Boolean CheckUserCodeIsValid(String code)
        {
            return userDAO.CheckUserCodeIsValid(code);
        }
    }
}
