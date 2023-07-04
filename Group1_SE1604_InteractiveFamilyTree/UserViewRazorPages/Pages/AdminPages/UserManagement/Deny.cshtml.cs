using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using BussinessObject.Models;

namespace UserViewRazorPages.Pages.AdminPages.UserManagement
{
    public class DenyModel : PageModel
    {
        IAccountReportRepository accountReportRepository = new AccountReportRepository();
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountReport accountReport = accountReportRepository.GetAccountReport(id.GetValueOrDefault());
            accountReport.StatusId = 3;
            accountReportRepository.Update(accountReport);

            return RedirectToPage("/AdminPages/UserManagement/UserReportManagement");
        }
    }
}
