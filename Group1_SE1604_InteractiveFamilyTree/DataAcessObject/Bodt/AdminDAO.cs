using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Bodt
{
    public class AdminDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();
        public Admin GetAdmin(int adminId)
        {
            Admin admin = new Admin();
            try
            {
                admin = context.Admins.FirstOrDefault(od => od.AdminId == adminId);;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return admin;
        }
        public Admin GetAdminbyEmail(string email)
        {
            Admin admin = new Admin();
            try
            {
                admin = context.Admins.FirstOrDefault(od => od.Email.ToLower().Equals(email.ToLower()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return admin;
        }
        public void AddAdmin(Admin admin)
        {
            if (admin == null)
            {
                throw new Exception("Admin is undefined!!");
            }
            try
            {
                if (GetAdmin(admin.AdminId) == null)
                {
                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Admin is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Admin admin)
        {
            if (admin == null)
            {
                throw new Exception("User is undefined!!");
            }
            try
            {
                Admin a = GetAdmin(admin.AdminId);
                if (a != null)
                {
                    context.Entry(a).State = EntityState.Detached;
                    context.Admins.Update(admin);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Admin does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
