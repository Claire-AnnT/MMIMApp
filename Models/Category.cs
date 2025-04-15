using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    public class Category
    {
        [Key]
        public ushort Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }
        [Required]
        public uint CreatedByUserId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public User CreatedByUser { get; set; } = null!;

        public Category(ushort id, string name,User createdByUser)
        {
            Id = id;
            Name = name;
            CreatedByUser = createdByUser;
            CreatedByUserId = createdByUser.Id;
        }

    }
}
