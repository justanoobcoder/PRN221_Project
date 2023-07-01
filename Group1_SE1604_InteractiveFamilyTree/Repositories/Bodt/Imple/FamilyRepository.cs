using BussinessObject.Models;
using DataAcessObject.Bodt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Bodt.Imple
{
    public class FamilyRepository : IFamilyRepository
    {
        FamilyDAO familyDAO = new FamilyDAO();
        public int GetNextFamilyId()
        {
            return familyDAO.GetNextFamilyId();
        }
        public int CreateNewFamily(Family family)
        {
            return familyDAO.CreateNewFamily(family);
        }
        public Family GetFamily(int familyId)
        {
            return familyDAO.GetFamily(familyId);
        }
        public void EditFamilyDetail(Family family) => familyDAO.EditFamilyDetail(family);
        public List<Family> GetAllFamilies()
        {
            return familyDAO.GetAllFamilies();
        }
    }
}
