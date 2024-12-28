using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public interface IUserService
    {
        bool Login(User user);
        List<Currency> GetCurrencies(); // Add this method to fetch available currencies
    }

}
