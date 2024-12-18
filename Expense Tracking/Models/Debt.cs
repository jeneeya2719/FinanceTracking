using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_Tracking.Models
{
    public class Debt
    {
        public Guid DebtId { get; set; } = Guid.NewGuid();
        public string Source { get; set; }
        public decimal DebtAmount { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
