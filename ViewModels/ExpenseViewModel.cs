using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseApp.Models;
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

    public ExpenseViewModel()
    {
        //todo expense viewmodel

        Debug.WriteLine("expense view moooooodel");

        SaveCommand = new RelayCommand(SaveProcess);
        DeleteCommand = new RelayCommand<ExpenseModel>(DeleteProcess);

        LoadItems(); //-->OnAppearing
    }

    private void DeleteProcess(ExpenseModel model)
    {
        Items.Remove(model);
        //todo remove from database
    }

    private async void SaveProcess()
    {
        Debug.WriteLine("saaaaave");
        if (isNew == true)
        {
            if (Items.Count == 0)
            {
                Item.Id = 1;
            }
            else
            {
                Item.Id = Items.Last().Id + 1;
            }
            Items.Add(Item);

            //todo save to database
        } else
        {
            int index = Items.IndexOf( 
                                Items.Where(x => x.Id == Item.Id).Single() 
                                );

            Items[index] = Item;
            //todo update database
        }

    }

    public void LoadItems()
    {
        Debug.WriteLine("===LoadItems====");

        Items.Clear();
        //todo read from database

        
        ExpenseModel exp1 = new()
        {
            Id = 1,
            Name = "exodo 1",
            Total = 100,
            CategoryId = 1
        };

        Items.Add(exp1);

        ExpenseModel exp2 = new()
        {
            Id = 2,
            Name = "exodo 2",
            Total = 222,
            CategoryId = 1
        };

        Items.Add(exp2);

        ExpenseModel exp3 = new()
        {
            Id = 3,
            Name = "exodo 3",
            Total = 123,
            CategoryId = 1
        };

        Items.Add(exp3);

        Items.Add(new ExpenseModel
        {
            Id = 4,
            Name = "exodo 4",
            Total = 44,
            CategoryId = 1
        });

        Items.Add(new ExpenseModel
        {
            Id = 5,
            Name = "exodo 5",
            Total = 505,
            CategoryId = 1
        });
        
    }

}
