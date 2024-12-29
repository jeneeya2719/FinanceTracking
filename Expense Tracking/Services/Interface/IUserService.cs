using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public interface IUserService
    {
        bool Login(User user);
        List<Currency> GetCurrencies();  // Method to get available currencies
    }

}
