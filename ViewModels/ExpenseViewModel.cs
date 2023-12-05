using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpenseApp.ViewModels;

public partial class ExpenseViewModel: ObservableObject
{
    [ObservableProperty]
    ObservableCollection<ExpenseModel> items = new();

    [ObservableProperty]
    ExpenseModel item = new();

    public bool isNew = false;

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    ApplicationDbContext Db = new();

    public ExpenseViewModel()
    {
        Debug.WriteLine("expense view moooooodel");

        SaveCommand = new RelayCommand(SaveProcess);
        DeleteCommand = new RelayCommand<ExpenseModel>(DeleteProcess);

        LoadItems(); 
    }

    private void DeleteProcess(ExpenseModel model)
    {
        Items.Remove(model);
        //todo remove from database
        Db.SaveChanges();
    }

    private async void SaveProcess()
    {
        Debug.WriteLine("saaaaave");
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
       
    }

    public void LoadItems()
    {
        Debug.WriteLine("===LoadItems====");

        Items.Clear();
        //todo read from database

        List<ExpenseModel> myList = Db.Expenses
                                        .Include(categ=>categ.Category)
                                        .OrderBy(exp=> exp.Name)
                                        .ToList();
        // select * from expenses
        // inner join categories on expenses.categoryid=categories.id
        // order by expenses.name

        foreach(ExpenseModel model in myList)
        {
            Items.Add(model);
        }

    }

}
