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
    internal class EditProductViewModel : ViewModelBase
    {
        private readonly MMIMAppContext context;

        private readonly Product productToEdit;

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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)SaveProductCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveProductCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LogoutCommand { get; }

        public EditProductViewModel(Product product)
        {
            context = new MMIMAppContext();

            productToEdit = product;

            ProductName = product.Name;
            Brand = product.Brand;
            UnitPrice = product.UnitPrice.ToString();
            CurrentUnits = product.Units.ToString();
            MinUnits = product.MinUnit.ToString();
            SelectedItemId = product.Category.Id;

            List<Category> categories = context.Categories.ToList();
            foreach (var category in categories)
            {
                ItemSource.Add(category);
            }

            SaveProductCommand = new RelayCommand(SaveProduct, CanSave);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            LogoutCommand = new RelayCommand(Logout, CanLogout);
        }

        private bool CanSave(object obj)
        {
            if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(UnitPrice) || string.IsNullOrWhiteSpace(CurrentUnits))
                return false;
            if (decimal.TryParse(UnitPrice, out decimal unitPrice) && int.TryParse(CurrentUnits, out int currentUnits))
            {
                if (unitPrice <= 0)
                    return false;
            }
            else return false;
            return true;
        }

        private void SaveProduct(object obj)
        {
            productToEdit.Name = ProductName;
            productToEdit.Brand = Brand;
            productToEdit.UnitPrice = decimal.Parse(UnitPrice);
            productToEdit.Units = int.Parse(CurrentUnits);
            productToEdit.MinUnit = int.TryParse(MinUnits, out int minUnitsParsed) ? minUnitsParsed : 0;
            productToEdit.Category = context.Categories.FirstOrDefault(c => c.Id == SelectedItemId);

            context.Products.Update(productToEdit);
            context.SaveChanges();

            // Navigates to SearchProductView
        }

        private bool CanCancel(object obj) => true;

        private void Cancel(object obj)
        {
            //Navigates to SearchProductView
        }

        private bool CanLogout(object obj) => true;

        private void Logout(object obj)
        {
            // Logs user out and navigates to login view
        }
    }
}
