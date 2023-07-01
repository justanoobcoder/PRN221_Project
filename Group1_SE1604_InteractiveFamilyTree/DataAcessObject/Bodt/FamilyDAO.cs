using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Bodt
{
    public class FamilyDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();

        public int GetNextFamilyId()
        {
            int nextFamilyId = -1;

            try
            {
                nextFamilyId = context.Families.Max(u => u.FamilyId) + 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nextFamilyId;
        }
        public int CreateNewFamily(Family family)
        {
            int newFamilyId = GetNextFamilyId();
            if (family == null)
            {
                throw new Exception("Family is undefined!!");
            }
            try
            {
                if (GetFamily(family.FamilyId) == null)
                {
                    context.Families.Add(family);
                    context.SaveChanges();
                    return family.FamilyId;
                }
                else
                {
                    throw new Exception("Family is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newFamilyId;
            }
         public Family GetFamily(int familyId)
            {
                Family family = null;

                try
                {
                    family = context.Families.SingleOrDefault(u => u.FamilyId == familyId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return family;
            }
         public void EditFamilyDetail(Family family)
        {

            if (family == null)
            {
                throw new Exception("Family is undefined!!");
            }
            try
            {
                Family f = GetFamily(family.FamilyId);
                if (f != null)
                {
                    context.Entry(f).State = EntityState.Detached;
                    context.Families.Update(family);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Family does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Family> GetAllFamilies()
        {
            List<Family> families = new List<Family>(); 
            try
            {
                families = context.Families.ToList();
                    context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return families;
        }
    }
    }
