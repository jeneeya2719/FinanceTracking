using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Expense_Tracking.Components.Pages
{
    public partial class Home
    {

       

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

        private void Logout()
        {
            Nav.NavigateTo("/login");
        }

    }
}