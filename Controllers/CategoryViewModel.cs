using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMIMApp.Controllers
{
    public class CategoryViewModel : ViewModelBase
    {
        public class AddCategoryViewModel : CategoryViewModel
        {
            private string categoryName = null!;
            public string CategoryName
            {
                get => categoryName;
                set
                {
                    categoryName = value;
                    OnPropertyChanged(nameof(CategoryName));
                }
            }

            public ICommand AddCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;

            public AddCategoryViewModel()
            {

            }

        }

        public class EditCategoryViewModel : CategoryViewModel
        {
            private string categoryName = null!;
            public string CategoryName
            {
                get => categoryName;
                set
                {
                    categoryName = value;
                    OnPropertyChanged(nameof(CategoryName));
                }
            }
            public ICommand SaveCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;

            public EditCategoryViewModel()
            {
            }
        }
    }
}
