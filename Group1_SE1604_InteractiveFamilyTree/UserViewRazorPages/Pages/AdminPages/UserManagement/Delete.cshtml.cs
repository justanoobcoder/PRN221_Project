using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.AdminPages.UserManagement
{
    public class DeleteModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IRelationshipRepository relationshipRepository = new RelationshipRepository();

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int? id)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId") ?? 0;
            if (adminId == 0)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUser(id.GetValueOrDefault());

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUser(id.GetValueOrDefault());


            if (User != null)
            {
                bool userCheck = relationshipRepository.CheckBelongUser(id.GetValueOrDefault());
                if (!userCheck)
                {
                    relationshipRepository.Delete(id.GetValueOrDefault());
                    TempData["notification"] = "Success!!!";
                }
                else
                {
                    TempData["notification"] = "Can not delete bacause there are users belong to this user!!!";
                }
            }

            return RedirectToPage("./UserReportManagement");
        }
    }
}
