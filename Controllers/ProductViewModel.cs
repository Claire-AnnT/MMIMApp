using System.Collections.ObjectModel;
using System.Windows.Input;
using MMIMApp.Models;

namespace MMIMApp.Controllers
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CategoriesViewModel> categories;

        public ProductViewModel()
        {
            categories = new ObservableCollection<CategoriesViewModel>();
        }

        public class AddProductViewModel : ProductViewModel
        {
            

            private string productName = null!;
            public string ProductName
            {
                get => productName;
                set
                {
                    productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }

            private string brand = null!;
            public string Brand
            {
                get => brand;
                set
                {
                    brand = value;
                    OnPropertyChanged(nameof(Brand));
                }
            }

            private int selectedCategoryId;
            public int SelectedCategoryId
            {
                get => selectedCategoryId;
                set
                {
                    selectedCategoryId = value;
                    OnPropertyChanged(nameof(SelectedCategoryId));
                }
            }

            private string? description;
            public string? Description
            {
                get => description;
                set
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }

            private string unitPrice = null!;
            public string UnitPrice
            {
                get => unitPrice;
                set
                {
                    unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }

            private string currentUnits = null!;
            public string CurrentUnits
            {
                get => currentUnits;
                set
                {
                    currentUnits = value;
                    OnPropertyChanged(nameof(CurrentUnits));
                }
            }

            private string minUnits = null!;
            public string MinUnits
            {
                get => minUnits;
                set
                {
                    minUnits = value;
                    OnPropertyChanged(nameof(MinUnits));
                }
            }

            public IEnumerable<CategoriesViewModel> Categories => categories;
       

            public ICommand AddCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;

            public AddProductViewModel()
            {
                
            }
        }

        public class EditProductViewModel : ProductViewModel
        {
            private string productName = null!;
            public string ProductName
            {
                get => productName;
                set
                {
                    productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }

            private string brand = null!;
            public string Brand
            {
                get => brand;
                set
                {
                    brand = value;
                    OnPropertyChanged(nameof(Brand));
                }
            }

            private string? description;
            public string? Description
            {
                get => description;
                set
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }

            private string unitPrice = null!;
            public string UnitPrice
            {
                get => unitPrice;
                set
                {
                    unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }

            private string currentUnits = null!;
            public string CurrentUnits
            {
                get => currentUnits;
                set
                {
                    currentUnits = value;
                    OnPropertyChanged(nameof(CurrentUnits));
                }
            }

            private string minUnits = null!;
            public string MinUnits
            {
                get => minUnits;
                set
                {
                    minUnits = value;
                    OnPropertyChanged(nameof(MinUnits));
                }
            }

            private int selectedCategoryId;
            public int SelectedCategoryId
            {
                get => selectedCategoryId;
                set
                {
                    selectedCategoryId = value;
                    OnPropertyChanged(nameof(SelectedCategoryId));
                }
            }

            public ICommand SaveCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;

            public EditProductViewModel()
            {
            }
        }

        public class ViewProductViewModel : ProductViewModel
        {
            private string productName = null!;
            public string ProductName
            {
                get => productName;
            }
            private string brand = null!;
            public string Brand
            {
                get => brand;
            }
            private string description = null!;
            public string Description
            {
                get => description;
            }
            private string unitPrice = null!;
            public string UnitPrice
            {
                get => unitPrice;
            }
            private string currentUnits = null!;
            public string CurrentUnits
            {
                get => currentUnits;
            }

            private string productCategory = null!;
            public string ProductCategory
            {
                get => productCategory;
            }

            private string status = null!;
            public string Status
            {
                get => status;
            }

            public ViewProductViewModel()
            {
            }
        }
    }
}
