using Microsoft.AspNetCore.Components;

namespace Expense_Tracking.Components.Pages
{
    public partial class Index : ComponentBase
    {
        protected override void OnInitialized()
        {
            Nav.NavigateTo("/login");
        }

    }
} 