using Expense_Tracking.Models;
using System.Text.Json;

namespace Expense_Tracking.Services
{
    public static class FinanceService
    {
        private static void SaveAll(Guid userId, List<Transaction> finances)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string todosFilePath = Utils.GetTodosFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(finances);
            File.WriteAllText(todosFilePath, json);
        }

        public static List<Transaction> GetAll(Guid userId)
        {
            string todosFilePath = Utils.GetTodosFilePath(userId);
            if (!File.Exists(todosFilePath))
            {
                return new List<Transaction>();
            }

            var json = File.ReadAllText(todosFilePath);

            return JsonSerializer.Deserialize<List<Transaction>>(json);
        }

        public static List<Transaction> Create(Guid userId, string taskName, DateTime dueDate)
        {
            if (dueDate < DateTime.Today)
            {
                throw new Exception("Due date must be in the future.");
            }

            List<Transaction> finances = GetAll(userId);
            finances.Add(new Transaction
            {
                TaskName = taskName,
                DueDate = dueDate,
                CreatedBy = userId
            });
            SaveAll(userId, finances);
            return finances;
        }

        public static List<Transaction> Delete(Guid userId, Guid id)
        {
            List<Transaction> finances = GetAll(userId);
            Transaction finance = finances.FirstOrDefault(x => x.Id == id);

            if (finance == null)
            {
                throw new Exception("Transacction not found.");
            }

            finances.Remove(finance);
            SaveAll(userId, finances);
            return finances;
        }

        public static void DeleteByUserId(Guid userId)
        {
            string todosFilePath = Utils.GetTodosFilePath(userId);
            if (File.Exists(todosFilePath))
            {
                File.Delete(todosFilePath);
            }
        }

        public static List<Transaction> Update(Guid userId, Guid id, string taskName, DateTime dueDate, bool isDone)
        {
            List<Transaction> finances = GetAll(userId);
            Transaction todoToUpdate = finances.FirstOrDefault(x => x.Id == id);

            if (todoToUpdate == null)
            {
                throw new Exception("Transaction not found.");
            }

            todoToUpdate.TaskName = taskName;
            todoToUpdate.IsDone = isDone;
            todoToUpdate.DueDate = dueDate;
            SaveAll(userId, finances);
            return finances;
        }
    }
}
