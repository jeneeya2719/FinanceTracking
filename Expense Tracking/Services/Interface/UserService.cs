﻿using Expense_Tracking.Enums;
using Expense_Tracking.Models;
using Expense_Tracking.Services.Interface;
using System.Text.Json;

namespace Expense_Tracking.Services
{
    public static class UserService
    {
        public const string SeedUsername = "admin";
        public const string SeedPassword = "admin";

        private static void SaveAll(List<User> users)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(appUsersFilePath, json);
        }

        public static List<User> GetAll()
        {
            string appUsersFilePath = Utils.GetAppUsersFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<User>>(json);
        }

        public static List<User> Create(Guid userId, string username, string password)
        {
            List<User> users = GetAll();
            bool usernameExists = users.Any(x => x.UserName == username);

            if (usernameExists)
            {
                throw new Exception("Username already exists.");
            }

            users.Add(
                new User
                {
                    UserName = username,
                    Password = Utils.HashSecret(password),
                    CreateDate = userId,
                 
                }
            );
            SaveAll(users);
            return users;
        }

        public static void SeedUsers()
        {
            var users = GetAll().FirstOrDefault(x => x.Currency == CurrencyCode.NPR);

            if (users == null)
            {
                Create(Guid.Empty, SeedUsername, SeedPassword);
            }
        }

        public static User GetById(Guid id)
        {
            List<User> users = GetAll();
            return users.FirstOrDefault(x => x.UserId == id);
        }

        public static List<User> Delete(Guid id)
        {
            List<User> users = GetAll();
            User user = users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            FinanceService.DeleteByUserId(id);
            users.Remove(user);
            SaveAll(users);

            return users;
        }

        public static User Login(string username, string password)
        {
            var loginErrorMessage = "Invalid username or password.";
            List<User> users = GetAll();
            User user = users.FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                throw new Exception(loginErrorMessage);
            }

            bool passwordIsValid = Utils.VerifyHash(password, user.Password);

            if (!passwordIsValid)
            {
                throw new Exception(loginErrorMessage);
            }

            return user;
        }

        public static User ChangePassword(Guid id, string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
            {
                throw new Exception("New password must be different from current password.");
            }

            List<User> users = GetAll();
            User user = users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool passwordIsValid = Utils.VerifyHash(currentPassword, user.Password);

            if (!passwordIsValid)
            {
                throw new Exception("Incorrect current password.");
            }

            user.Password = Utils.HashSecret(newPassword);
            user.HasInitialPassword = false;
            SaveAll(users);

            return user;
        }
    }
}
