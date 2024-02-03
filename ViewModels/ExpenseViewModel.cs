using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseApp.Data;
using ExpenseApp.Models;
using ExpenseApp.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application = Microsoft.Maui.Controls.Application;

namespace ExpenseApp.ViewModels;

public partial class ExpenseViewModel: ObservableObject
{
    [ObservableProperty]
    ObservableCollection<ExpenseModel> items = new();

    [ObservableProperty]
    ObservableCollection<CategoryModel> categories = new();

    [ObservableProperty]
    bool loading;

    [ObservableProperty]
    ExpenseModel item = new();

    public bool isNew = false;

    ApplicationDbContext Db = new();

    public ExpenseViewModel()
    {
        Debug.WriteLine( nameof(ExpenseViewModel) );

        Task.Run(async () =>
        {
            Loading = true;
            await LoadItems();
            await LoadCategories();
            Loading = false;

        }).ConfigureAwait(false);
        
    }

    [RelayCommand]
    async Task Delete(ExpenseModel model)
    {
        Debug.WriteLine(nameof(Delete));
        bool confirm = await Application.Current.MainPage
                    .DisplayAlert("Warning", "Are you sure?", "Yes", "No");

        if (confirm)
        {
            Items.Remove(model);
            await Db.SaveChangesAsync();
        }
    }

    [RelayCommand]
    async Task Add()
    {
        Debug.WriteLine(nameof(Add));
        isNew = true;
        Item = new();
        await Shell.Current.Navigation.PushAsync(new ExpensePage(this));
    }

    [RelayCommand]
    async Task Update(ExpenseModel model)
    {
        Debug.WriteLine(nameof(Update));
        isNew = false;
        Item = model;
        await Shell.Current.Navigation.PushAsync(new ExpensePage(this));
    }

    [RelayCommand]
    async Task Save()
    {
        Debug.WriteLine(nameof(Save));

        if (isNew == true)
        {
            Db.Expenses.Add(Item);
            await Db.SaveChangesAsync();

            Items.Add(Item);

        } else
        {
            int index = Items.IndexOf( 
                                Items.Where(x => x.Id == Item.Id).Single() 
                                );

            Items[index] = Item;

            await Db.SaveChangesAsync();
        }

        await Shell.Current.Navigation.PopAsync();
    }

    protected async Task LoadItems()
    {
        Debug.WriteLine(nameof(LoadItems));

        Items.Clear();
        //todo read from database

        List<ExpenseModel> myList = await Db.Expenses
                                        .Include(categ => categ.Category)
                                        .OrderBy(exp => exp.Name)
                                        .ToListAsync();
        // query executed:
        // select * from expenses
        // inner join categories on expenses.categoryid=categories.id
        // order by expenses.name

        foreach (ExpenseModel model in myList)
        {
            Items.Add(model);
        }

    }

    protected async Task LoadCategories()
    {
        Debug.WriteLine(nameof(LoadCategories));

        Categories.Clear();

        List<CategoryModel> myList = await Db.Categories
                                            .ToListAsync();

        foreach (CategoryModel model in myList)
        {
            Categories.Add(model);
        }

    }

}
