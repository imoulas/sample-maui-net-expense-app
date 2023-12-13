using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Models;

/// <summary>
/// This is the main expense model
/// </summary>
public partial class ExpenseModel :ObservableObject
{
    [Key]
    public int Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    int total;

    [ObservableProperty]
    CategoryModel category;
}
