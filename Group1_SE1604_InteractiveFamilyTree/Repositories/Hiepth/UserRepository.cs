using BussinessObject.Models;
using DataAcessObject.Hiepth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Hiepth
{
    public class UserRepository : IUserRepository
    {
        public User GetById(int id) => UserDAO.Instance.GetById(id);

        public List<User> GetUsersHaveAccountByFamilyId(int familyId) => UserDAO.Instance.GetUsersHaveAccountByFamilyId(familyId);
    }
}
