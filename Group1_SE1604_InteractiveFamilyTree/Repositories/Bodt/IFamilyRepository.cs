using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt
{
    public interface IFamilyRepository
    {
        public int GetNextFamilyId();
        public int CreateNewFamily(Family family);
        public Family GetFamily(int familyId);
        public void EditFamilyDetail(Family family);
        public List<Family> GetAllFamilies();
    }
}
