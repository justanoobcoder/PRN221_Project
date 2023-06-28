using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccesObject.Dangptm
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static object instanceLook = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
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

        public Admin LoginAdmin(string email, string password)
        {
            Admin admin = new Admin();
            try
            {

                admin = context.Admins.SingleOrDefault(x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return admin;
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
                customer = context.Users.SingleOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }

        public User GetUserByCode(string code)
        {
            User customer = null;

            try
            {
                customer = context.Users.SingleOrDefault(u => u.Code.ToLower().Equals(code.ToLower()));
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
                if (GetUser(user.UserId) == null && GetUserByEmail(user.Email) == null)
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
                if (u != null && GetUserByEmail(user.Email)== null)
                {
                    context.Entry(u).State = EntityState.Detached;
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Email has been used!");
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
    }

}


