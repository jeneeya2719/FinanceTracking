using Expense_Tracking.Models;
using System.Text.Json;

namespace Expense_Tracking.Services.Interface
{
    public static class FinanceService
    {
        private static void SaveAll(Guid userId, List<FinanceItem> finac)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string todosFilePath = Utils.GetTodosFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(finac);
            File.WriteAllText(todosFilePath, json);
        }

        public static List<FinanceItem> GetAll(Guid userId)
        {
            string todosFilePath = Utils.GetTodosFilePath(userId);
            if (!File.Exists(todosFilePath))
            {
                return new List<FinanceItem>();
            }

            var json = File.ReadAllText(todosFilePath);

            return JsonSerializer.Deserialize<List<FinanceItem>>(json);
        }

        public static List<FinanceItem> Create(Guid userId, string taskName, DateTime dueDate)
        {
            if (dueDate < DateTime.Today)
            {
                throw new Exception("Due date must be in the future.");
            }

            List<FinanceItem> todos = GetAll(userId);
            todos.Add(new FinanceItem
            {
                TaskName = taskName,
                DueDate = dueDate,
                CreatedBy = userId
            });
            SaveAll(userId, todos);
            return todos;
        }

        public static List<FinanceItem> Delete(Guid userId, Guid id)
        {
            List<FinanceItem> todos = GetAll(userId);
            FinanceItem todo = todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
            {
                throw new Exception("Todo not found.");
            }

            todos.Remove(todo);
            SaveAll(userId, todos);
            return todos;
        }

        public static void DeleteByUserId(Guid userId)
        {
            string todosFilePath = Utils.GetTodosFilePath(userId);
            if (File.Exists(todosFilePath))
            {
                File.Delete(todosFilePath);
            }
        }

        public static List<FinanceItem> Update(Guid userId, Guid id, string taskName, DateTime dueDate, bool isDone)
        {
            List<FinanceItem> todos = GetAll(userId);
            FinanceItem todoToUpdate = todos.FirstOrDefault(x => x.Id == id);

            if (todoToUpdate == null)
            {
                throw new Exception("Todo not found.");
            }

            todoToUpdate.TaskName = taskName;
            todoToUpdate.IsDone = isDone;
            todoToUpdate.DueDate = dueDate;
            SaveAll(userId, todos);
            return todos;
        }
    }
}
