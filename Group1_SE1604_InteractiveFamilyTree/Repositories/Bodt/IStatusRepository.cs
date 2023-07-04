using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt
{
    public interface IStatusRepository
    {
        public Status GetStatus(int statusId);
    }
}
