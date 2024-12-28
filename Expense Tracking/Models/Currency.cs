using Expense_Tracking.Enums;

namespace Expense_Tracking.Models
{
    public class Currency
    {
        public Guid CurrencyId { get; set; } = Guid.NewGuid();
        public CurrencyCode CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
