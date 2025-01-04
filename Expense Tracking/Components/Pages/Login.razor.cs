using Expense_Tracking.Models;

namespace Expense_Tracking.Components.Pages
{
    public partial class Login
    {
        private User Users = new User();
        private List<Currency> AvailableCurrencies { get; set; } = new List<Currency>();
        private Guid SelectedCurrencyId { get; set; }
        private string ErrorMessge { get; set; }

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
                ErrorMessge = "Please fill all fields.";
                return;
            }

            Users.CurrencyId = SelectedCurrencyId; // Set the currency to the user object

            bool isValid = UserService.Login(Users);
            if (isValid)
            {
                // Set the current user in the global state
                GlobalState.CurrentUser = Users;
                Nav.NavigateTo("/dashboard");
            }
            else
            {
                ErrorMessge = "Invalid login credentials.";
            }
        }
    }
}
