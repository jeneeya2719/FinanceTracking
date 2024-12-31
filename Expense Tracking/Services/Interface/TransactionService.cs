using Expense_Tracking.Models;
using Expense_Tracking.Services.Interface;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Expense_Tracking.Services
{
    public class TransactionService : ITransactionService
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");

        // Load transactions from the JSON file
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            if (!File.Exists(FilePath)) return new List<Transaction>();
            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        // Add a transaction to the list and save to JSON
        public async Task AddTransactionAsync(Transaction transaction)
        {
            var transactions = await GetTransactionsAsync();
            transactions.Add(transaction);
            await SaveTransactionsAsync(transactions);
        }

        // Update an existing transaction
        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var transactions = await GetTransactionsAsync();
            var existingTransaction = transactions.FirstOrDefault(t => t.TransactionId == transaction.TransactionId);

            if (existingTransaction != null)
            {
                existingTransaction.TransactionTitle = transaction.TransactionTitle;
                existingTransaction.TransactionType = transaction.TransactionType;
                existingTransaction.Source = transaction.Source;
                existingTransaction.TransactionAmount = transaction.TransactionAmount;
                existingTransaction.Date = transaction.Date;
                existingTransaction.Remarks = transaction.Remarks;
                await SaveTransactionsAsync(transactions);
            }
        }

        // Delete a transaction
        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            var transactions = await GetTransactionsAsync();
            var transactionToDelete = transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transactionToDelete != null)
            {
                transactions.Remove(transactionToDelete);
                await SaveTransactionsAsync(transactions);
            }
        }

        // Save the list of transactions to the JSON file
        private async Task SaveTransactionsAsync(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
