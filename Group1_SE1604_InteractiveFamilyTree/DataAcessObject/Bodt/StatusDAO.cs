using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Bodt
{
    public class StatusDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();

        public Status GetStatus(int statusId)
        {
            Status status;
            try
            {
                status = context.Statuses.FirstOrDefault(od => od.Id == statusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }
    }
}
