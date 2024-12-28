using Expense_Tracking.Abstraction;
using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public class UserService : UserBase, IUserService
    {
        private List<User> _users;
        private List<Currency> _currencies;

        public const string SeedUsername = "admin";
        public const string SeedPassword = "password";

        public UserService()
        {
            _users = LoadUsers();
            _currencies = SeedCurrencies();

            if (!_users.Any())
            {
                _users.Add(new User
                {
                    UserName = SeedUsername,
                    Password = SeedPassword,
                    CurrencyId = _currencies.First().CurrencyId // Default currency
                });
                SaveUsers(_users);
            }
        }

        // Fetch currencies for dropdown
        public List<Currency> GetCurrencies() => _currencies;

        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || user.CurrencyId == Guid.Empty)
            {
                return false;
            }

            return _users.Any(u => u.UserName == user.UserName && u.Password == user.Password && u.CurrencyId == user.CurrencyId);
        }

        private List<Currency> SeedCurrencies()
        {
            return new List<Currency>
            {
                new Currency { CurrencyId = Guid.NewGuid(), CurrencyName = "Nepalese Rupee" },
                new Currency { CurrencyId = Guid.NewGuid(), CurrencyName = "US Dollar" },
                new Currency { CurrencyId = Guid.NewGuid(), CurrencyName = "Euro" },
                new Currency { CurrencyId = Guid.NewGuid(), CurrencyName = "Australian Dollar" }
            };
        }
    }
}
