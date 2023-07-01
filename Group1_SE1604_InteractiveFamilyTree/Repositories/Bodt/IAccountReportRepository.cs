using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt
{
    public interface IAccountReportRepository
    {
        public void CreateNewReport(AccountReport accountReport);
        public List<AccountReport> showAccountReport(int userId);
    }
}
