using Expense_Tracking.Enums;

namespace Expense_Tracking.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Password { get; set; }
       

        // Foreign Key for Currency
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        //Relationship between tables
        public ICollection<Transaction> Transactions { get; set; }
    }   
}
