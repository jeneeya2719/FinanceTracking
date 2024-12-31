﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracking.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        // Add all the necessary fields here
        public string TransactionTitle { get; set; }

        public string TransactionType { get; set; }

        public string Source { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime Date { get; set; } // Changed to DateTime for proper date binding

        public string Remarks { get; set; }

        // Foreign Keys for tag
        [ForeignKey("Tag")]
        public Guid? TagId { get; set; }
        public Tag Tag { get; set; }

        // Foreign Keys for user
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Foreign Keys for debt
        [ForeignKey("Debt")]
        public Guid? DebtId { get; set; }
        public Debt Debt { get; set; }
    }
}
