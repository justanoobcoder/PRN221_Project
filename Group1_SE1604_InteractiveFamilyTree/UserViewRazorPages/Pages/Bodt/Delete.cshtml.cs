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

namespace UserViewRazorPages.Pages.Bodt
{
    public class DeleteModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        IAccountReportRepository accountReportRepository = new AccountReportRepository();
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public String Reason { get; set; }

        public IActionResult OnGet(int id)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            if (id == null)
            {
                return NotFound();
            }
            User = userRepository.GetUser(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUser(id.GetValueOrDefault());
            if (User != null)
            {
                AccountReport accountReport = new AccountReport();
                accountReport.UserId = id.GetValueOrDefault();
                accountReport.ReporterId = userId;
                accountReport.Reason = Reason;
                accountReport.StatusId = 1;
                accountReport.DateReported = DateTime.Now;
                accountReportRepository.CreateNewReport(accountReport);
            }


                /*if (User != null)
                {
                    bool userCheck = relationshipRepository.CheckBelongUser(id);
                    if (!userCheck)
                    {
                        relationshipRepository.Delete(id);
                        TempData["notification"] = "Success!!!";
                    }
                    else
                    {
                        TempData["notification"] = "Can not delete bacause there are users belong to this user!!!";
                    }
                }*/
                return RedirectToPage("/Bodt/MainPage");
        }
    }
}
