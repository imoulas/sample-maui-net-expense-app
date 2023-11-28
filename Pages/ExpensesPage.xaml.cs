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

    private void AddExpenseBtn_Clicked(object sender, EventArgs e)
    {
        ExpenseViewModel viewModel = (ExpenseViewModel)BindingContext;
        viewModel.isNew = true;
        viewModel.Item = new();
        Shell.Current.Navigation.PushAsync(new ExpensePage(viewModel));
       
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //ExpenseViewModel viewModel = (ExpenseViewModel)BindingContext;
        //viewModel.LoadItems();

    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ExpenseModel expenseModel = e.CurrentSelection.SingleOrDefault() as ExpenseModel;

        ExpenseViewModel viewModel = (ExpenseViewModel)BindingContext;
        viewModel.isNew = false;
        viewModel.Item = expenseModel;

        //CollectionView myCollectionView = (CollectionView)sender;
        //myCollectionView.SelectedItem = null;

        await Shell.Current.Navigation.PushAsync(new ExpensePage(viewModel));


    }
}