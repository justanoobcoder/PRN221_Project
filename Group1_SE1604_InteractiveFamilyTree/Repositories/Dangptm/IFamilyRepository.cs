using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dangptm
{
    public interface IFamilyRepository
    {
        public List<Family> GetFamilieList();
    }
}
