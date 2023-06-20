using BussinessObject.Models;
using DataAccesObject.Dangptm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dangptm
{
    public class UserRepository : IUserRepository
    {
        public User Login(string email, string password)
            => UserDAO.Instance.Login(email, password);

        public Admin LoginAdmin(string email, string password)
            => UserDAO.Instance.LoginAdmin(email, password);

        public void AddUser(User user)
            => UserDAO.Instance.AddUser(user);

        public void Update(User user)
            => UserDAO.Instance.Update(user);

        public User GetUserByCode(string code)
            => UserDAO.Instance.GetUserByCode(code);

        public User GetUser(int userId)
            => UserDAO.Instance.GetUser(userId);    
    }
}
