using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAcessObject.Bodt
{
    public class UserDAO
    {
        FamilyTreeContext context = new FamilyTreeContext();
        public List<User> GetUserList()
        {
            List<User> users = null;

            try
            {
                // Get From Database
                users = context.Users.ToList();
                // Add Default Costumer

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }
        public List<User> GetUserListByFamilyId(int familyId)
        {
            List<User> users = null;

            try
            {
                // Get From Database
                users = context.Users.Where(od => od.FamilyId == familyId).ToList();
                // Add Default Costumer

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public User Login(string email, string password)
        {
            User user = new User();
            try
            {

                user = context.Users.SingleOrDefault(x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public User GetUser(int userId)
        {
            User user = null;

            try
            {
                user = context.Users.SingleOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            User customer = null;

            try
            {
                customer = context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User is undefined!!");
            }
            try
            {
                if (GetUser(user.UserId) == null)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User user)
        {
            if (user == null)
            {
                throw new Exception("User is undefined!!");
            }
            try
            {
                User u = GetUser(user.UserId);
                if (u != null)
                {
                    context.Entry(u).State = EntityState.Detached;
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Delete(int userId)
        {
            try
            {
                User customer = GetUser(userId);
                if (customer != null)
                {
                    context.Users.Remove(customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<User> searchUser(string search)
        {
            List<User> customers = null;

            try
            {
                var filteredObjects = from obj in context.Users
                                      where obj.Name.ToLower().Contains(search.ToLower())
                                      select obj;
                var result = filteredObjects.ToList();
                customers = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customers;
        }
        public int getFamilyId(int userId)
        {
            int id;

            try
            {
                id = context.Users.FirstOrDefault(od => od.UserId == userId).FamilyId.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return id;
        }
        public int familyCount(int familyId)
        {
            int count;
            try
            {
                count = context.Users.Where(od => od.FamilyId == familyId).ToList().Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }
        public List<User> getMarriedUser(int FamilyId)
        {
            List<User> List;
            try
            {
                var query = from user in context.Users
                            join rel in context.Relationships
                                on user.UserId equals rel.UserId1 into userRelationships1
                            from ur1 in userRelationships1.DefaultIfEmpty()
                            join rel in context.Relationships
                                on user.UserId equals rel.UserId2 into userRelationships2
                            from ur2 in userRelationships2.DefaultIfEmpty()
                            where ((ur1 != null && ur1.RelationshipDetailId == 3) ||
                                  (ur2 != null && ur2.RelationshipDetailId == 3)) && user.FamilyId == 1
                            select user;


                List = query.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return List;
        }
        public List<User> getUnavailable(int FamilyId)
        {
            List<User> List;
            try
            {
                var query = from user in context.Users
                            join rel in context.Relationships
                                on user.UserId equals rel.UserId2 into userRelationships
                            from ur in userRelationships.DefaultIfEmpty()
                            where ur != null && ur.RelationshipDetailId == 3 && user.FamilyId == 1
                            select user;


                List = query.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return List;
        }
        public Boolean CheckUserCodeIsValid(String code)
        {
            Boolean check;
            try
            {
                User user = context.Users.FirstOrDefault(od => od.Code.Equals(code));
                if (user != null)
                    check = false;
                else check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }
    }
    
}


