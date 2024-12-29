using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public interface ICurrencyService
    {
        List<Currency> GetCurrencies();
    }
}
