namespace Expense_Tracking.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        public string TransactionTitle { get; set; }

        public string TransactionType { get; set; }

        public string Category { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateOnly Date { get; set; }

        public string Remarks { get; set; }


        // Foreign Keys for tag
        public Guid? TagId { get; set; }  // Nullable

        public Debt Tag { get; set; }

        // Foreign Keys for user
        public Guid UserId { get; set; }

        public User User { get; set; }

        // Foreign Keys for debt
        public Guid? DebtId { get; set; } 
        // Nullable
        public Debt Debt { get; set; }

        
    }
}
