using ExpenseApp.ViewModels;

namespace ExpenseApp.Pages;

public partial class ExpensePage : ContentPage
{
	public ExpensePage(ExpenseViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        ExpenseViewModel viewModel = (ExpenseViewModel)BindingContext;
        if (viewModel.SaveCommand.CanExecute(null))
        {
            viewModel.SaveCommand.Execute(null);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}