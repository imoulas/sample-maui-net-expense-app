using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Models;

/// <summary>
/// This is the category model
/// </summary>
public partial class CategoryModel :ObservableObject
{
    [Key]
    public int Id { get; set; }

    [ObservableProperty]
    string name;
  
}
