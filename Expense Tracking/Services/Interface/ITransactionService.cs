using Expense_Tracking.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense_Tracking.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllAsync(Guid userId, string tabFilter, string sortBy, string sortDirection, string searchTerm);
        Task<List<Transaction>> CreateAsync(Guid userId, string taskName, DateTime dueDate);
        Task<List<Transaction>> UpdateAsync(Guid userId, Guid id, string taskName, DateTime dueDate, bool isDone);
        Task<List<Transaction>> DeleteAsync(Guid userId, Guid id);
        Task DeleteByUserIdAsync(Guid userId);
    }
}
