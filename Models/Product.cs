using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string? Description { get; set; }
        
        [Column (TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Unit { get; set; }
        public int MinUnit { get; set; }
        
        public User User { get; set; } = null!;
        public Category? Category { get; set; }
    }
}
