using Expense_Tracking.Models;

namespace Expense_Tracking.Components.Pages
{
    public partial class Login
    {
        private User Users = new User();
        private List<Currency> AvailableCurrencies { get; set; } = new List<Currency>();
        private Guid SelectedCurrencyId { get; set; }
        private string ErrorMessge { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AvailableCurrencies = CurrencyService.GetCurrencies();
            if (AvailableCurrencies.Any())
            {
                SelectedCurrencyId = AvailableCurrencies.First().CurrencyId;
            }
        }

         private void  HandleLogin()
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
                Nav.NavigateTo("/home"); 
            }
            else
            {
                ErrorMessge = "Invalid login credentials.";
            }
        }
    }
}
