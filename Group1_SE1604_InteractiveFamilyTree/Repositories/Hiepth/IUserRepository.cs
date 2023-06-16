using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Hiepth
{
    public interface IUserRepository
    {
        List<User> GetUsersHaveAccountByFamilyId(int familyId);
        User GetById(int id);
    }
}
