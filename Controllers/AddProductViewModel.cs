using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MMIMApp.Commands;
using MMIMApp.Data;
using MMIMApp.Models;

namespace MMIMApp.Controllers
{
    public class AddProductViewModel : ViewModelBase
    {
        private readonly MMIMAppContext context;

        public ObservableCollection<Category> ItemSource { get; set; } = new ObservableCollection<Category>();

        private string productName = string.Empty;
        public string ProductName
        {
            get { return productName; }
            set
            { 
                if (productName != value)
                {
                    productName = value;
                    OnPropertyChanged(nameof(ProductName));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string brand = string.Empty;
        public string Brand
        {
            get { return brand; }
            set
            {
                if (brand != value)
                {
                    brand = value;
                    OnPropertyChanged(nameof(Brand));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private ushort selectedItemId;
        public ushort SelectedItemId
        {
            get { return selectedItemId; }
            set
            {
                if (selectedItemId != value)
                {
                    selectedItemId = value;
                    OnPropertyChanged(nameof(SelectedItemId));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }


        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string unitPrice = string.Empty;
        public string UnitPrice
        {
            get { return unitPrice; }
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string currentUnits = string.Empty;
        public string CurrentUnits
        {
            get { return currentUnits; }
            set
            {
                if (currentUnits != value)
                {
                    currentUnits = value;
                    OnPropertyChanged(nameof(CurrentUnits));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string? minUnits = string.Empty;
        public string? MinUnits
        {
            get { return minUnits; }
            set
            {
                if (minUnits != value)
                {
                    minUnits = value;
                    OnPropertyChanged(nameof(MinUnits));
                    ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand AddProductCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LogoutCommand { get; }

        public AddProductViewModel()
        {
            context = new MMIMAppContext();

            List<Category> categories = context.Categories.ToList();

            
            
            foreach (var category in categories)
            {
                ItemSource.Add(category);
            }

            if (!context.Categories.Any())
            {
                
                for (int i = 0; i < 25; i++)
                {
                    Category newCategory = new Category($"Category {i}");
                    context.Categories.Add(newCategory);
                }

                context.SaveChanges();
            }

            AddProductCommand = new RelayCommand(AddProduct,CanAddProduct);
            CancelCommand = new RelayCommand(Cancel,CanCancel);
        }

        private bool CanAddProduct(object obj)
        {
            if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(UnitPrice) || string.IsNullOrWhiteSpace(CurrentUnits))
                return false;
            if (decimal.TryParse(UnitPrice, out decimal unitPrice) && int.TryParse(CurrentUnits, out int currentUnits))
            {
                if (unitPrice <= 0 )
                    return false;
            }
            else
                return false;
            return true;
        }

        private void AddProduct(object obj)
        {
            Product newProduct = new Product(ProductName, Brand, decimal.Parse(UnitPrice), int.Parse(CurrentUnits), int.TryParse(MinUnits, out int minUnits) ? minUnits : 0, context.Categories.FirstOrDefault(c => c.Id == SelectedItemId));
            context.Products.Add(newProduct);
            context.SaveChanges();
            //Navigates to SearchProductView
        }

        private bool CanCancel(object obj) => true;

        private void Cancel(object obj)
        {
            //Navigates to SearchProductView
        }
    }
}
