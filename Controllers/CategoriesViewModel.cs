using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMIMApp.Models;

namespace MMIMApp.Controllers
{
     public class CategoriesViewModel : ViewModelBase
    {

        private readonly Category category;
        public ushort Id => category.Id;
        public string Name => category.Name;

        public CategoriesViewModel(Category category)
        {
            this.category = category;
        }
    }
}
