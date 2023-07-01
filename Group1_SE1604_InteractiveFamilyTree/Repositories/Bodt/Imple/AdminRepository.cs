using BussinessObject.Models;
using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public class AdminRepository : IAdminRepository
    {
        AdminDAO adminDAO = new AdminDAO();
        public Admin GetAdmin(int adminId)
        {
            return adminDAO.GetAdmin(adminId);
        }
        public void AddAdmin(Admin admin) => adminDAO.AddAdmin(admin);

        public void Update(Admin admin) => adminDAO.Update(admin);  
    }
}
