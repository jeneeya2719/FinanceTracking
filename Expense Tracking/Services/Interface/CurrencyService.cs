using Expense_Tracking.Enums;
using Expense_Tracking.Models;

namespace Expense_Tracking.Services.Interface
{
    public class CurrencyService : ICurrencyService
    {
        private readonly List<Currency> _currencies = new()
        {
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.NPR },
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.USD},
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.EUR },
            new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = CurrencyCode.AUD }
        };

        //public Task<List<Currency>> GetAllCurrenciesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public List<Currency> GetCurrencies()
        {
            return _currencies;
        }


    }
}
