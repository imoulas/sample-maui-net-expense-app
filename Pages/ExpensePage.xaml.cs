using ExpenseApp.ViewModels;

namespace ExpenseApp.Pages;

public partial class ExpensePage : ContentPage
{
	public ExpensePage(ExpenseViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }

}