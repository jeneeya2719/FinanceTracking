using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Expense_Tracking.Components.Pages
{
    public partial class Home
    {

        // State variables to control visibility and dialog state
        private bool isTransactionPageVisible = false;
        private bool isAddTodoDialogOpen = false;
        private string newTodoTitle;

        // Toggles the visibility of the Transaction page content
        private void ShowTransactionPage()
        {
            isTransactionPageVisible = !isTransactionPageVisible;
        }

        // Opens the add todo dialog
        private void OpenAddTodoDialog()
        {
            isAddTodoDialogOpen = true;
        }

        // Closes the add todo dialog
        private void CloseAddTodoDialog()
        {
            isAddTodoDialogOpen = false;
        }

        // Add todo logic
        private void AddTodo()
        {
            if (!string.IsNullOrEmpty(newTodoTitle))
            {
                // Logic to add the todo (e.g., saving it to a list or database)
                Console.WriteLine($"Added Todo: {newTodoTitle}");
                newTodoTitle = string.Empty; // Reset the input field
                CloseAddTodoDialog(); // Close the dialog
            }
        }

        private void NavigateToTransaction()
        {
            // Navigate to the transaction page
            Nav.NavigateTo("/transaction");
        }

        // Logic for logout
        private async Task AskForLogout()
        {
            // Trigger a confirmation box and check the user's choice
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to logout?");

            // Proceed with logout only if confirmed is true (OK clicked)
            if (confirmed)
            {
                Logout();
            }
            else
            {
                Nav.NavigateTo("/dashboard");
            }
        }

        // Logout logic
        private void Logout()
        {
            Nav.NavigateTo("/login");
        }
    }
}
