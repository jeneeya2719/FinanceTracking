using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_Tracking.Models
{
    public class Currency
    {
        public Guid CurrencyId { get; set; } = Guid.NewGuid();
        public string CurrencyCode { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
