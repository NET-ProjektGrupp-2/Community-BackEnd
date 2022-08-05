using Community_BackEnd.Entities;
using Microsoft.AspNetCore.Identity;

namespace Community_BackEnd.Services
{
    public interface IUserService
    {
        AppUser Authenticate(string username, string password);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(int id);
    }
    //Task<string> RegisterAsync(RegisterModel model);
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //List<IdentityUserRole<string>> _usersroles = List < IdentityUserRole<string> >
        private List<AppUser> _users = new List<AppUser>
        {
            //new AppUser {  Firstname = "Admin", Surname = "User", CreationDate = DateTime.Now, },
          //  new IdentityUserRole { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User }
        };
        public AppUser Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppUser GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
