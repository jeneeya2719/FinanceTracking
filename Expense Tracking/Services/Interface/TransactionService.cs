using Expense_Tracking.Abstraction;
using Expense_Tracking.Models;
using System.Text.Json;

namespace Expense_Tracking.Services
{
    public class TransactionService : UserBase
    {
        private static async Task SaveAllAsync(Guid userId, List<Transaction> transactions)
        {
            string appDataDirectoryPath = UserBase.GetAppDirectoryPath();
            string todosFilePath = UserBase.GetTodosFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(transactions);
            await File.WriteAllTextAsync(todosFilePath, json);
        }

        public async Task<List<Transaction>> GetAllAsync(Guid userId, string tabFilter, string sortBy, string sortDirection, string searchTerm)
        {
            string todosFilePath = UserBase.GetTodosFilePath(userId);
            if (!File.Exists(todosFilePath))
            {
                return new List<Transaction>();
            }

            var json = await File.ReadAllTextAsync(todosFilePath);
            var transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();

            // Apply filters and sorting (you can extend these based on your needs)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                transactions = transactions.Where(t => t.TaskName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (sortBy == "DueDate")
            {
                transactions = sortDirection == "asc"
                    ? transactions.OrderBy(t => t.DueDate).ToList()
                    : transactions.OrderByDescending(t => t.DueDate).ToList();
            }

            return transactions;
        }

       /* public async Task<List<Transaction>> CreateAsync(Transaction trans)
        {

             List<Transaction> transactions = await GetAllAsync(trans.Id, string.Empty, string.Empty, string.Empty, string.Empty);

             transactions.Add(new Transaction
             {
                 Id = new Guid(),
                 TaskName = trans.TaskName
             });
             await SaveUsers(transactions);
             return transactions;
         }*/
        public async Task<List<Transaction>> UpdateAsync(Guid userId, Guid id, string taskName, DateTime dueDate, bool isDone)
        {
            List<Transaction> transactions = await GetAllAsync(userId, string.Empty, string.Empty, string.Empty, string.Empty);
            Transaction transactionToUpdate = transactions.FirstOrDefault(x => x.Id == id);

            if (transactionToUpdate == null)
            {
                throw new Exception("Transaction not found.");
            }

            transactionToUpdate.TaskName = taskName;
            transactionToUpdate.IsDone = isDone;
            transactionToUpdate.DueDate = dueDate;
            await SaveAllAsync(userId, transactions);
            return transactions;
        }

        public async Task<List<Transaction>> DeleteAsync(Guid userId, Guid id)
        {
            List<Transaction> transactions = await GetAllAsync(userId, string.Empty, string.Empty, string.Empty, string.Empty);
            Transaction transaction = transactions.FirstOrDefault(x => x.Id == id);

            if (transaction == null)
            {
                throw new Exception("Transaction not found.");
            }

            transactions.Remove(transaction);
            await SaveAllAsync(userId, transactions);
            return transactions;
        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            string todosFilePath = UserBase.GetTodosFilePath(userId);
            if (File.Exists(todosFilePath))
            {
                await Task.Run(() => File.Delete(todosFilePath)); // File delete task
            }
        }
    }
}
