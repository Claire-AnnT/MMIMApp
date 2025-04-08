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
        
        public uint CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public Category(ushort id, string name, uint createdByUserId, User createdByUser)
        {
            Id = id;
            Name = name;
            CreatedByUserId = createdByUserId;
            CreatedByUser = createdByUser;
        }

    }
}
