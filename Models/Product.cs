using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Brand { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)"),Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Unit { get; set; }
        public int MinUnit { get; set; }
        [Required]
        public uint CreatedByUserId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public User CreatedByUser { get; set; } = null!;
        public ushort? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public Product(int id, string name, string brand, decimal unitPrice, int unit, int minUnit, User createdByUser, Category? category=null)
        {
            Id = id;
            Name = name;
            Brand = brand;
            UnitPrice = unitPrice;
            Unit = unit;
            MinUnit = minUnit;
            CreatedByUserId = createdByUser.Id;
            CreatedByUser = createdByUser;
            Category = category;
            CategoryId = category?.Id;
        }

        
    }
}
