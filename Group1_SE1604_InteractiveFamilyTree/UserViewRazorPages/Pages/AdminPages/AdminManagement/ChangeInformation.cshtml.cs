using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt;
using Repositories.Bodt.Imple;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.AdminPages.AdminManagement
{
    public class ChangeInformationModel : PageModel
    {
        IAdminRepository adminRepository = new AdminRepository();

        [BindProperty]
        public Admin Admin { get; set; }

        public IActionResult OnGet()
        {
            int adminId = HttpContext.Session.GetInt32("AdminId") ?? 0;
            if (adminId == 0)
            {
                return NotFound();
            }

            Admin = adminRepository.GetAdmin(adminId);

            if (Admin == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        { 
            try
            {
                adminRepository.Update(Admin);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(Admin.AdminId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/AdminPages/AdminManagement/ChangeInformation");
        }

        private bool AdminExists(int id)
        {
            Admin admin = adminRepository.GetAdmin(id);
            if (admin != null)
                return true;
            return false;
        }
    }
}
