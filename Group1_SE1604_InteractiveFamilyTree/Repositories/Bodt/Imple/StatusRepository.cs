using BussinessObject.Models;
using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public class StatusRepository : IStatusRepository
    {
        StatusDAO statusDAO = new StatusDAO();
        public Status GetStatus(int statusId)
        {
            return statusDAO.GetStatus(statusId);   
        }
    }
}
