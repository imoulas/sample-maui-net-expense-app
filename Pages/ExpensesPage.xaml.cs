using ExpenseApp.Models;
using ExpenseApp.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;

namespace ExpenseApp.Pages;

public partial class ExpensesPage : ContentPage
{
	public ExpensesPage()
	{
		InitializeComponent();

		BindingContext = new ExpenseViewModel();
	}
}