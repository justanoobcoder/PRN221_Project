using FamilyTree.Models;
using FamilyTree.DAO;

namespace FamilyTree.Repository.Imple
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user) => UserDAO.Instance.AddUser(user);

        public void DeleteUser(int userId) => UserDAO.Instance.Delete(userId);


        public User GetUser(int userId)
        {
            return UserDAO.Instance.GetUser(userId);
        }

        public User GetUserByEmail(string email)
        {
            return UserDAO.Instance.GetUserByEmail(email);
        }

        public List<User> GetUserList()
        {
            return UserDAO.Instance.GetUserList();
        }

        public User Login(string email, string password)
        {
            return UserDAO.Instance.Login(email, password);
        }

        public void UpdateCustomer(User user) => UserDAO.Instance.Update(user);

        public List<User> searchUser(string search)
        {
            return UserDAO.Instance.searchUser(search);
        }
    }
}
