using Expense_Tracking.Enums;
using Expense_Tracking.Models;
using Expense_Tracking.Services;

namespace Expense_Tracking.Components.Pages
{
    public partial class Login
    {
        private User Users = new User();
        private List<Currency> AvailableCurrencies { get; set; } = new List<Currency>();
        private Guid SelectedCurrencyId { get; set; }
        private string ErrorMessage { get; set; } // Fixed typo in 'ErrorMessge'

        // Initialization of available currencies
        protected override async Task OnInitializedAsync()
        {
            AvailableCurrencies = UserService.GetCurrencies();  // Get currencies directly from UserService
            if (AvailableCurrencies.Any())
            {
                SelectedCurrencyId = AvailableCurrencies.First().CurrencyId;
            }
        }

        // Handle login logic
        private void HandleLogin()
        {
            if (string.IsNullOrEmpty(Users.UserName) || string.IsNullOrEmpty(Users.Password) || SelectedCurrencyId == Guid.Empty)
            {
                ErrorMessage = "Please fill all fields."; // Use the correct error message property
                return;
            }

            // Set the selected currency to the user object (optional, depending on the implementation of the User model)
            Users.Currency = AvailableCurrencies.FirstOrDefault(c => c.CurrencyId == SelectedCurrencyId)?.CurrencyCode ?? CurrencyCode.NPR;

            try
            {
                // Try to login the user
                User loggedInUser = UserService.Login(Users.UserName, Users.Password);

                // If successful, set the current user and navigate to the dashboard
                GlobalState.CurrentUser = loggedInUser;
                Nav.NavigateTo("/dashboard");
            }
            catch (Exception ex)
            {
                // If login fails, show the error message
                ErrorMessage = ex.Message; // Using the exception message as the error
            }
        }
    }
}
