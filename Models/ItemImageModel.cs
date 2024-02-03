using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Models;

/// <summary>
/// This is the item image model
/// </summary>
public partial class ItemImageModel : ObservableObject
{
    [Key]
    public int Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string color;

    [ObservableProperty]
    string imageName;

    [ObservableProperty]
    int layoutHeight;
}
