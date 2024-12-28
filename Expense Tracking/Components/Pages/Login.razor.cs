using Expense_Tracking.Models;

namespace Expense_Tracking.Components.Pages
{
    public partial class Login
    {
        private string? ErrorMessage;
        public User Users { get; set; } = new();
        private List<Currency> Currencies { get; set; } = new(); // List to hold currencies

        //[Inject] public UserService UserService { get; set; } // Inject UserService
        //[Inject] public NavigationManager Nav { get; set; } // Inject NavigationManager to navigate to home

        // On Initialization, load the currencies from the service
        protected override void OnInitialized()
        {
            Currencies = UserService.GetCurrencies(); // Get available currencies for dropdown
        }

        // Handle the login action
        private void HandleLogin()
        {
            // Ensure the user has selected a valid currency, username, and password
            if (string.IsNullOrEmpty(Users.UserName) || string.IsNullOrEmpty(Users.Password) || Users.CurrencyId == Guid.Empty)
            {
                ErrorMessage = "All fields are required.";
                return;
            }

            // Check login credentials
            if (UserService.Login(Users))
            {
                Nav.NavigateTo("/home"); // Redirect to home on successful login
            }
            else
            {
                ErrorMessage = "Invalid username, password, or currency selection."; // Error message on failure
            }
        }
    }
}
