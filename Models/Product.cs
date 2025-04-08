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
        
        public uint CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public ushort CategoryId { get; set; }
        public Category? Category { get; set; }

        public Product(int id, string name, string brand, decimal unitPrice, int unit, int minUnit, uint createdByUserId, User createdByUser)
        {
            Id = id;
            Name = name;
            Brand = brand;
            UnitPrice = unitPrice;
            Unit = unit;
            MinUnit = minUnit;
            CreatedByUserId = createdByUserId;
            CreatedByUser = createdByUser;
        }

        public void setCategory(ushort categoryId, Category category)
        {
            if (categoryId == 0)
            {
                throw new ArgumentException("Category ID cannot be 0");
            }
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            
            CategoryId = categoryId;
            Category = category;
        }

        public void addStock(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0");
            }
            Unit += amount;
        }

        public void removeStock(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0");
            }
            Unit -= amount;
            if (Unit < MinUnit)
            {
                lowStockWarning();
            }
            else if (Unit <= 0)
            {
                emptyStockWarning();
            }
        }

        public string lowStockWarning()
        {
            if (Unit < MinUnit)
            {
                return $"Warning: {Name} is low on stock. Only {Unit} units left.";
            }
            return $"{Name} is in stock.";
        }

        public string emptyStockWarning()
        {
            if (Unit <= 0)
            {
                return $"Warning: {Name} has no more stock.";
            }
            return $"{Name} is in stock.";
        }
    }
}
