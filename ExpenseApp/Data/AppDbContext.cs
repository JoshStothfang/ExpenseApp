using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Models;

namespace ExpenseApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = default!;

        public DbSet<Expense> Expenses { get; set; } = default!;
        
        public DbSet<ExpenseLine> ExpenseLines { get; set; } = default!;
        
        public DbSet<ExpenseApp.Models.Item> Items { get; set; } = default!;
    }
}
