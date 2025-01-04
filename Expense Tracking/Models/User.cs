using Expense_Tracking.Enums;

namespace Expense_Tracking.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool HasInitialPassword { get; set; }

        public CurrencyCode Currency { get; set; }

        public Guid CreateDate { get; set; }    

        //Relationship between tables
        //public ICollection<Transaction> Transactions { get; set; }
    }
}
