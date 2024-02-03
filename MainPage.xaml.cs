using ExpenseApp.Pages;

namespace ExpenseApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void ExpensesBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new ExpensesPage());
        }

        private async void MasonryBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new MasonryPage());
        }
    }

}
