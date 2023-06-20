using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Common
{
    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode(User obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}
