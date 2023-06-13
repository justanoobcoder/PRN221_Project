using FamilyTree.DAO;
using FamilyTree.Models;

namespace FamilyTree.Repository
{
    public interface IUserRepository
    {
        public void AddUser(User user);

        public void DeleteUser(int userId);


        public User GetUser(int userId);

        public User GetUserByEmail(string email);

        public List<User> GetUserList();
        public User Login(string email, string password);

        public void UpdateCustomer(User user);

        public List<User> searchUser(string search);
    }
}
