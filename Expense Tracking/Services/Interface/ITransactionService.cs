using Expense_Tracking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense_Tracking.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync();
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Guid transactionId);
    }
}
