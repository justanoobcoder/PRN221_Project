using BussinessObject.Models;
using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public class AccountReportRepository : IAccountReportRepository
    {
        AccountReportDAO accountReportDAO = new AccountReportDAO();
        public void CreateNewReport(AccountReport accountReport)
            => accountReportDAO.CreateNewReport(accountReport);
        public List<AccountReport> showAccountReport(int userId)
        {
            return accountReportDAO.showAccountReport(userId);
        }
        public List<AccountReport> GetAccountReports()
        {
            return accountReportDAO.GetAccountReports();
        }
        public AccountReport GetAccountReport(int accountReportId)
        {
            return accountReportDAO.GetAccountReport(accountReportId);
        }
        public void Update(AccountReport accountReport) => accountReportDAO.Update(accountReport);
    }
}
