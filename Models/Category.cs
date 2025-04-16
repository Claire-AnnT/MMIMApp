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
    public class Category : INotifyPropertyChanged
    {
        [Key]
        public ushort Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }

        public Category()
        {

        }
        
        public Category(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
