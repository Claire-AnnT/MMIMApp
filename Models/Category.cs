using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    class Category
    {
        public ushort Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }
    }
}
