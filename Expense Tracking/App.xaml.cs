using Expense_Tracking.Models;

namespace Expense_Tracking
{
    public partial class App : Application
    {
        public static GlobalState GlobalState { get; private set; } = new GlobalState();

        public App()
        {
            InitializeComponent();

            // Set the main page
            MainPage = new MainPage();
        }
    }
}
