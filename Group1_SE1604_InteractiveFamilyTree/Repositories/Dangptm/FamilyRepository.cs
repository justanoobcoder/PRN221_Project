using BussinessObject.Models;
using DataAcessObject.Dangptm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dangptm
{
    public class FamilyRepository : IFamilyRepository
    {
        public List<Family> GetFamilieList()
            => FamilyDAO.Instance.GetFamilieList();
    }
}
