using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ExpenseApp.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ExpenseApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApp.ViewModels;

public partial class MasonryViewModel : ObservableObject
{

    [ObservableProperty]
    ObservableCollection<ItemImageModel> items = new();

    ApplicationDbContext Db = new();

    public MasonryViewModel()
	{
        Debug.WriteLine(nameof(MasonryViewModel));

        LoadItems();
    }

    protected  void LoadItems()
    {
        Debug.WriteLine(nameof(LoadItems));

        Items.Clear();
        //todo read from database

        List<ItemImageModel> myList = Db.ItemImages
                                        .ToList();
     
        foreach (ItemImageModel model in myList)
        {
            Items.Add(model);
        }

    }

}

