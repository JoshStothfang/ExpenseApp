using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseApp.Models
{
    [Index("EmployeeId", IsUnique = true)]
    public class Expense
    {
        public int Id { get; set; }
        [StringLength(80)]
        public string Description { get; set; } = string.Empty;
        [StringLength(10)]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0;
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<ExpenseLine>? ExpenseLines { get;}
    }
}
