using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataAcessObject.Bodt
{
    public class AccountReportDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();

        public void CreateNewReport(AccountReport accountReport)
        {
            if (accountReport == null)
            {
                throw new Exception("Account Report is undefined!!");
            }
            try
            {
                context.Entry(accountReport).State = EntityState.Detached;
                context.AccountReports.Add(accountReport);
                context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AccountReport> showAccountReport(int userId)
        {
            List<AccountReport> result = new List<AccountReport>();
            if (userId == 0)
            {
                throw new Exception("Can not find!");
            }
            try
            {
                result = context.AccountReports.Where(od => od.ReporterId == userId).ToList();
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
