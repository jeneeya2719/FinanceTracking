using Expense_Tracking.Abstraction;
using Expense_Tracking.Enums;
using Expense_Tracking.Models;
using Expense_Tracking.Services.Interface;

namespace Expense_Tracking.Services
{
    public class UserService : UserBase, IUserService
    {
        private readonly List<User> _users;
        private static readonly List<Currency> _currencies = new()
        {
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.NPR },
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.USD },
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.EUR },
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.AUD }
        };

        public const string SeedUsername = "jeneey";
        public const string SeedPassword = "jeneey";

        public UserService()
        {
            _users = LoadUsers();

            if (!_users.Any())
            {
                _users.Add(new User { UserName = SeedUsername, Password = SeedPassword });
                SaveUsers(_users);
            }
        }

        // Method to handle user login and validate credentials
        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            var authenticatedUser = _users.FirstOrDefault(u =>
                u.UserName == user.UserName &&
                u.Password == user.Password);  //till here

            if (authenticatedUser != null)
            {
                // Assign currency to the user after successful login
                user.CurrencyId = authenticatedUser.CurrencyId;
                return true;
            }

            return false;
        }

        // Method to fetch available currencies
        public List<Currency> GetCurrencies() => _currencies;

        

        //after second seeding error occur so this method call, which check is the list is empty, reload users and save the updated users
        //Withoout this method in memory the first seeded value store and dont get updated.
        private List<User> LoadUsers()
        {
            return new List<User>(); 
        }

        // Save users to persistent storage
        private void SaveUsers(List<User> users)
        {
            
        }
    }
}
