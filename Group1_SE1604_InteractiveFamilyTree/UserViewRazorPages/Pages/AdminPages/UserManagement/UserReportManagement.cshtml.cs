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
using System.Security.Cryptography.Xml;

namespace UserViewRazorPages.Pages.AdminPages.UserManagement
{
    public class UserReportManagementModel : PageModel
    {
        IAccountReportRepository accountReportRepository = new AccountReportRepository();
        IUserRepository userRepository = new UserRepository();
        IStatusRepository statusRepository = new StatusRepository();

        public IList<AccountReport> AccountReport { get;set; }

        public IActionResult OnGet()
        {
            AccountReport = accountReportRepository.GetAccountReports();
            foreach(var report in AccountReport)
            {
                report.Reporter = userRepository.GetUser(report.ReporterId);
                report.Status = statusRepository.GetStatus(report.StatusId.GetValueOrDefault());
                report.User = userRepository.GetUser(report.UserId);
            }
            return Page();
        }
    }
}
