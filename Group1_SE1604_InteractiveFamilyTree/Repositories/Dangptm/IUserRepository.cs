using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dangptm
{
    public interface IUserRepository
    {
        public User Login(string email, string password);
        public Admin LoginAdmin(string email, string password);
        public void AddUser(User user);
        public void Update(User user);
        public User GetUserByCode(string code);
        public User GetUser(int userId);
    }
}
