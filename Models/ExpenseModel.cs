using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Models;

/// <summary>
/// This is the main expense model
/// </summary>
public class ExpenseModel:ObservableObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Total { get; set; }
    public int CategoryId { get; set; }
}
