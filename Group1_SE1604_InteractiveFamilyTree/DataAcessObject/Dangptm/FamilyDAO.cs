using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Dangptm
{
    public class FamilyDAO
    {
        private static FamilyDAO instance = null;
        private static object instanceLook = new object();

        public static FamilyDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new FamilyDAO();
                    }
                    return instance;
                }
            }
        }
        FamilyTreeContext context = new FamilyTreeContext();

        public List<Family> GetFamilieList()
        {
            List<Family> families = null;

            try
            {
                // Get From Database
                families = context.Families.ToList();
                // Add Default Costumer

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return families;
        }
    }
}
