using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public interface IUserRepository
    {
        public void AddUser(User user);

        public void DeleteUser(int userId);


        public User GetUser(int userId);

        public User GetUserByEmail(string email);

        public List<User> GetUserList();
        public User Login(string email, string password);

        public void Update(User user);

        public List<User> searchUser(string search);

        public List<User> GetUserListByFamilyId(int familyId);
        public int familyCount(int familyId);
        public List<User> getMarriedUser(int FamilyId);
        public List<User> getUnavailable(int FamilyId);
        public Boolean CheckUserCodeIsValid(String code);
    }
}
