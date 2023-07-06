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
    public class AccountReportListModel : PageModel
    {
        IAccountReportRepository accountReportRepository = new AccountReportRepository();
        IUserRepository userRepository = new UserRepository();

        public IList<AccountReport> AccountReport { get;set; }

        public IActionResult OnGet()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            AccountReport = accountReportRepository.showAccountReport(userId);
            foreach (var accountReport in AccountReport)
            {
                accountReport.User = userRepository.GetUser(accountReport.UserId);
            }
            return Page();
        }
    }
}
