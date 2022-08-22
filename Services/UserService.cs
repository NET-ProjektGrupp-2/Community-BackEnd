using Community_BackEnd.Entities;
using Microsoft.AspNetCore.Identity;

namespace Community_BackEnd.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
    //Task<string> RegisterAsync(RegisterModel model);
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //List<IdentityUserRole<string>> _usersroles = List < IdentityUserRole<string> >
        private List<User> _users = new List<User>
        {
            //new AppUser {  Firstname = "Admin", Surname = "User", CreationDate = DateTime.Now, },
          //  new IdentityUserRole { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User }
        };
        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
