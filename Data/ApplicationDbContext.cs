using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Data;

public class ApplicationDbContext: DbContext
{
    //database tables
    public DbSet<ExpenseModel> Expenses { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ItemImageModel> ItemImages { get; set; }

    public string DBPath { get; private set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {

    }

    public ApplicationDbContext()
    {
        DBPath = $"{FileSystem.Current.AppDataDirectory}{System.IO.Path.DirectorySeparatorChar}expense.dat";
        Debug.WriteLine($"STORAGE EXPENSE DATABASE: {DBPath}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite($"Data Source={DBPath}");
    }
}
