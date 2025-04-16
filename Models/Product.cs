using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    public class Product : INotifyPropertyChanged
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
        private int units;
        [Required]
        public int Units
        {
            get => units;
            set
            {
                if (units != value)
                {
                    units = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MinUnit { get; set; }
        public ushort? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        
        
        public Product()
        {
        }

        public Product(string name, string brand, decimal unitPrice, int unit, int minUnit, Category? category=null)
        {
            Name = name;
            Brand = brand;
            UnitPrice = unitPrice;
            Units = unit;
            MinUnit = minUnit;
            Category = category;
            CategoryId = category?.Id;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
