﻿using Expense_Tracking.Models;
using System.Text.Json;

namespace Expense_Tracking.Abstraction
{
    public abstract class UserBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "users.json");

        protected List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

        }

        protected void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, json);
        }

        protected void SaveTransaction(List<Transaction> Transaction)
        {
            var json = JsonSerializer.Serialize(Transaction);
            File.WriteAllText(FilePath, json);
        }

        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Transaction-Data"
            );
        }

        public static string GetTodosFilePath(Guid userId)
        {
            return Path.Combine(GetAppDirectoryPath(), userId.ToString() + "_transactions.json");
        }
    }
}
