namespace Expense_Tracking.Models
{
    public class Tag
    {
        public Guid TagId { get; set; } = Guid.NewGuid();

        public string TagName { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
