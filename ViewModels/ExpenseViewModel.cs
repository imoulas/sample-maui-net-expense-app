﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    ExpenseModel item = new();

    public bool isNew = false;

    // CRUD Create-Read-Update-Remove
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand AddCommand { get;  }
    public ICommand UpdateCommand { get; }

    ApplicationDbContext Db = new();

    public ExpenseViewModel()
    {
        Debug.WriteLine( nameof(ExpenseViewModel) );

        SaveCommand = new RelayCommand(SaveProcess);
        DeleteCommand = new RelayCommand<ExpenseModel>(DeleteProcess);
        AddCommand = new RelayCommand(AddProcess);
        UpdateCommand = new RelayCommand<ExpenseModel>(UpdateProcess);

        LoadItems();
        LoadCategories();
    }

    private async void DeleteProcess(ExpenseModel model)
    {
        Debug.WriteLine(nameof(DeleteProcess));
        bool confirm = await Application.Current.MainPage
                    .DisplayAlert("Warning", "Are you sure?", "Yes", "No");

        if (confirm)
        {
            Items.Remove(model);
            Db.SaveChanges();
        }
    }

    private void AddProcess()
    {
        Debug.WriteLine(nameof(AddProcess));
        isNew = true;
        Item = new();
        Shell.Current.Navigation.PushAsync(new ExpensePage(this));
    }

    private void UpdateProcess(ExpenseModel model)
    {
        Debug.WriteLine(nameof(UpdateProcess));
        isNew = false;
        Item = model;
        Shell.Current.Navigation.PushAsync(new ExpensePage(this));
    }

    private void SaveProcess()
    {
        Debug.WriteLine(nameof(SaveProcess));

        if (isNew == true)
        {
            //todo save to database
            Db.Expenses.Add(Item);
            Db.SaveChanges();

            Items.Add(Item);

        } else
        {
            int index = Items.IndexOf( 
                                Items.Where(x => x.Id == Item.Id).Single() 
                                );

            Items[index] = Item;

            Db.SaveChanges();
        }
        Shell.Current.Navigation.PopAsync();
    }

    public void LoadItems()
    {
        Debug.WriteLine("===LoadItems===");

        Items.Clear();
        //todo read from database

        List<ExpenseModel> myList = Db.Expenses
                                        .Include(categ => categ.Category)
                                        .OrderBy(exp => exp.Name)
                                        .ToList();
        // select * from expenses
        // inner join categories on expenses.categoryid=categories.id
        // order by expenses.name

        foreach (ExpenseModel model in myList)
        {
               
            Items.Add(model);
               
        }
    }

    private void LoadCategories()
    {
        Categories.Clear();

        List<CategoryModel> myList = Db.Categories.ToList();
        foreach (CategoryModel model in myList)
        {
            Categories.Add(model);
        }

    }

}
