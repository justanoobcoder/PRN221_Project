using BussinessObject.Models;
using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
   public interface IAdminRepository
    {
        public Admin GetAdmin(int adminId);
        public void AddAdmin(Admin admin);
        public void Update(Admin admin);
    }
}
