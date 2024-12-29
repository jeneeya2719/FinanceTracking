using Expense_Tracking.Abstraction;
using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public class UserService : UserBase, IUserService
    {
        private readonly List<User> _users;

        public const string SeedUsername = "admin";
        public const string SeedPassword = "password";

        public UserService(ICurrencyService currencyService)
        {
            _users = LoadUsers();

            if (!_users.Any())
            {
                _users.Add(new User { UserName = SeedUsername, Password = SeedPassword });
                SaveUsers(_users);
            }
        }


        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return _users.Any(u =>
                u.UserName == user.UserName &&
                u.Password == user.Password);  
        }
    }
}
