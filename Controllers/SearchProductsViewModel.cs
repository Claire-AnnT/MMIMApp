using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using MMIMApp.Commands;
using MMIMApp.Data;
using MMIMApp.Models;
using MMIMApp.Views.Controls;
using MMIMApp.Views.ProductViews;
using static MMIMApp.Commands.RelayCommand;

namespace MMIMApp.Controllers
{
    class SearchProductsViewModel : ViewModelBase
    {
        private Product productToDelete;

        //Observable collections
        public ObservableCollection<Product> AllProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> PagedProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> FilteredProducts { get; private set; }

        //Properties
        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private bool successPopUpIsOpen;
        public bool SuccessPopUpIsOpen
        {
            get => successPopUpIsOpen;
            set
            {
                successPopUpIsOpen = value;
                OnPropertyChanged(nameof(SuccessPopUpIsOpen));
            }
        }

        private string successPopUpMessage;
        public string SuccessPopUpMessage
        {
            get => successPopUpMessage;
            set
            {
                successPopUpMessage = value;
                OnPropertyChanged(nameof(SuccessPopUpMessage));
            }
        }

        private bool errorPopUpIsOpen;
        public bool ErrorPopUpIsOpen
        {
            get => errorPopUpIsOpen;
            set
            {
                errorPopUpIsOpen = value;
                OnPropertyChanged(nameof(ErrorPopUpIsOpen));
            }
        }

        private string errorPopUpMessage;
        public string ErrorPopUpMessage
        {
            get => errorPopUpMessage;
            set
            {
                errorPopUpMessage = value;
                OnPropertyChanged(nameof(ErrorPopUpMessage));
            }
        }

        private bool confirmationPopUpIsOpen;
        public bool ConfirmationPopUpIsOpen
        {
            get => confirmationPopUpIsOpen;
            set
            {
                confirmationPopUpIsOpen = value;
                OnPropertyChanged(nameof(ConfirmationPopUpIsOpen));
            }
        }

        private string confirmationPopUpTitle;
        public string ConfirmationPopUpTitle
        {
            get => confirmationPopUpTitle;
            set
            {
                confirmationPopUpTitle = value;
                OnPropertyChanged(nameof(ConfirmationPopUpTitle));
            }
        }

        private string confirmationPopUpMessage;
        public string ConfirmationPopUpMessage
        {
            get => confirmationPopUpMessage;
            set
            {
                confirmationPopUpMessage = value;
                OnPropertyChanged(nameof(ConfirmationPopUpMessage));
            }
        }

        private string searchString;
        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
                Search(searchString);
            }
        }

        public string NumPageItems =>
            $"Page {CurrentPage} of {TotalPages} " +
            $"({PagedProducts.Count} items / {FilteredProducts?.Count ?? 0} total)";

        private int currentPage = 1;
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    UpdatePagedProducts();

                    (PreviousPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (NextPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private void UpdatePagedProducts()
        {
            if (FilteredProducts == null)
                return;

            PagedProducts.Clear();
            foreach (var product in FilteredProducts
                .Skip((CurrentPage - 1) * 5)
                .Take(5))
            {
                PagedProducts.Add(product);
            }

            OnPropertyChanged(nameof(PagedProducts));
            OnPropertyChanged(nameof(NumPageItems));
        }

        public int TotalPages =>
    (FilteredProducts != null && FilteredProducts.Count > 0)
        ? (int)Math.Ceiling((double)FilteredProducts.Count / 5)
        : 1;

        private void Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                FilteredProducts = new ObservableCollection<Product>(AllProducts);
                PagedProducts.Clear();
                foreach (var product in FilteredProducts.Take(5))
                {
                    PagedProducts.Add(product);
                }
            }
            else
            {
                var products = context.Products
                    .Where(p =>
                        p.Name != null && p.Name.ToLower().Contains(searchString.ToLower()) ||
                        p.Brand != null && p.Brand.ToLower().Contains(searchString.ToLower()))
                    .ToList();

                FilteredProducts = new ObservableCollection<Product>(products);
                PagedProducts.Clear();
                foreach (var product in products.Take(5))
                {
                    PagedProducts.Add(product);
                }
            }

            CurrentPage = 1;
            UpdatePagedProducts();
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
        }

        private readonly MMIMAppContext context;

        //Commands
        public ICommand AddUnitCommand { get; }
        public ICommand RemoveUnitCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand CloseSuccessPopup { get; }
        public ICommand ShowConfirmatioPopUp { get; }
        public ICommand CloseConfirmationPopup { get; }
        public ICommand CloseErrorPopup { get; }
        public ICommand ClearFiltersCommand { get; }
        public ICommand OutOfStockFilterCommand { get; }
        public ICommand InStockFilterCommand { get; }
        public ICommand LowStockFilterCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }



        //Constructor
        public SearchProductsViewModel()
        {
            context = new MMIMAppContext();
            context.Database.EnsureCreated();

            //Initialize collections
            List<Product> products = context.Products.ToList();
            AllProducts = new ObservableCollection<Product>(products);

            if (!context.Products.Any())
            {

                for (int i = 0; i < 25; i++)
                {
                    Product newProduct = new Product(
                        $"Product {i}",
                        $"Brand {i}",
                        0.00m,
                        3,
                        1,
                        null
                    );
                    context.Products.Add(newProduct);
                }

                context.SaveChanges();
            }

            FilteredProducts = new ObservableCollection<Product>(AllProducts);
            PagedProducts = new ObservableCollection<Product>(AllProducts.Take(5));

            //Initialize properties
            SelectedProduct = null!;
            SuccessPopUpIsOpen = false;
            ErrorPopUpIsOpen = false;
            ConfirmationPopUpIsOpen = false;
            SuccessPopUpMessage = string.Empty;
            ErrorPopUpMessage = string.Empty;
            ConfirmationPopUpMessage = string.Empty;
            ConfirmationPopUpTitle = $"Delete Product?";
           

            //Initialize commands
            AddUnitCommand = new RelayCommand(AddUnit, CanAddUnit);
            RemoveUnitCommand = new RelayCommand(RemoveUnit, CanRemoveUnit);
            EditProductCommand = new RelayCommand(EditProductObj, CanEditProductObj);
            DeleteCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            LogoutCommand = new RelayCommand(Logout, CanLogout);
            CloseSuccessPopup = new RelayCommand(CloseSuccessPopUp, CanCloseSuccessPopUp);
            CloseConfirmationPopup = new RelayCommand(CloseConfirmationPopUp, CanCloseConfirmationPopUp);
            CloseErrorPopup = new RelayCommand(CloseErrorPopUp, CanCloseErrorPopUp);
            ShowConfirmatioPopUp = new RelayCommand(ShowConfirmationPopUp, CanShowConfirmationPopUp);
            ClearFiltersCommand = new RelayCommand(ClearFilters, CanClearFilters);
            OutOfStockFilterCommand = new RelayCommand(OutOfStockFilter, CanOutOfStockFilter);
            InStockFilterCommand = new RelayCommand(InStockFilter, CanInStockFilter);
            LowStockFilterCommand = new RelayCommand(LowStockFilter, CanLowStockFilter);
            ExportCommand = new RelayCommand(Export, CanExport);
            NextPageCommand = new RelayCommand(NextPage, CanNextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage, CanPreviousPage);
        }

        private bool CanPreviousPage(object obj) => CurrentPage > 1;

        private void PreviousPage(object obj)
        {
            CurrentPage--;
        }

        private bool CanNextPage(object obj) => CurrentPage < TotalPages;

        private void NextPage(object obj)
        {
            CurrentPage++;
        }

        private bool CanExport(object obj) => AllProducts.Count > 0 && FilteredProducts.Count > 0;

        private void Export(object obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Product Name, Brand,Unit Price,Units,Min Unit,Description,Category");

            foreach (var product in context.Products)
            {
                sb.AppendLine($"{Escape(product.Name)},{Escape(product.Brand)},{product.UnitPrice},{product.Units},{product.MinUnit},{Escape(product.Description ?? "N/A")},{Escape(product.Category?.Name ?? "N/A")}");
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "Products.csv"
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    SuccessPopUpMessage = "Exported successfully.";
                    SuccessPopUpIsOpen = true;
                }
                catch
                {
                    ErrorPopUpMessage = "Error exporting file.";
                    ErrorPopUpIsOpen = true;
                }
            }
            else
            {
                ErrorPopUpMessage = "Export cancelled.";
                ErrorPopUpIsOpen = true;
            }
            
        }

        private string Escape(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "\"\"";
            return input.Contains(",") || input.Contains("\"") ? $"\"{input.Replace("\"", "\"\"")}\"" : input;
        }

        private bool CanLowStockFilter(object obj) => true;

        private void LowStockFilter(object obj)
        {
            var products = context.Products
        .Where(p => p.Units <= p.MinUnit && p.Units > 0 &&
                   (string.IsNullOrWhiteSpace(SearchString) ||
                    (p.Name != null && p.Name.ToLower().Contains(SearchString.ToLower())) ||
                    (p.Brand != null && p.Brand.ToLower().Contains(SearchString.ToLower()))))
        .ToList();

            FilteredProducts = new ObservableCollection<Product>(products);
            PagedProducts.Clear();
            foreach (var product in products.Take(5))
            {
                PagedProducts.Add(product);
            }

            CurrentPage = 1;
            UpdatePagedProducts();
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
        }

        private bool CanInStockFilter(object obj) => true;

        private void InStockFilter(object obj)
        {
            var products = context.Products
        .Where(p => p.Units > p.MinUnit &&
                   (string.IsNullOrWhiteSpace(SearchString) ||
                    (p.Name != null && p.Name.ToLower().Contains(SearchString.ToLower())) ||
                    (p.Brand != null && p.Brand.ToLower().Contains(SearchString.ToLower()))))
        .ToList();

            FilteredProducts = new ObservableCollection<Product>(products);
            PagedProducts.Clear();
            foreach (var product in products.Take(5))
            {
                PagedProducts.Add(product);
            }

            CurrentPage = 1;
            UpdatePagedProducts();
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
        }

        private bool CanOutOfStockFilter(object obj) => true;

        private void OutOfStockFilter(object obj)
        {
            var products = context.Products
        .Where(p => p.Units == 0 &&
                   (string.IsNullOrWhiteSpace(SearchString) ||
                    (p.Name != null && p.Name.ToLower().Contains(SearchString.ToLower())) ||
                    (p.Brand != null && p.Brand.ToLower().Contains(SearchString.ToLower()))))
        .ToList();

            FilteredProducts = new ObservableCollection<Product>(products);
            PagedProducts.Clear();
            foreach (var product in products.Take(5))
            {
                PagedProducts.Add(product);
            }

            CurrentPage = 1;
            UpdatePagedProducts();
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
        }

        private bool CanClearFilters(object obj) => true;

        private void ClearFilters(object obj)
        {
            var products = context.Products;
            PagedProducts.Clear();
            foreach (var product in products.Take(5))
            {
                PagedProducts.Add(product);
            }
            FilteredProducts = new ObservableCollection<Product>(products);

            CurrentPage = 1;
            UpdatePagedProducts();
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
            OnPropertyChanged(nameof(ClearFilters));
        }




        // Misc commands
        private bool CanShowConfirmationPopUp(object obj) => true;

        private void ShowConfirmationPopUp(object obj)
        {
            if(obj is Product product)
            {
                productToDelete = product;
                ConfirmationPopUpMessage = "Are you sure you want to delete this product?";
                ConfirmationPopUpIsOpen = true;
            }
        }

        private bool CanCloseErrorPopUp(object obj) => true;

        private void CloseErrorPopUp(object obj)
        {
            ErrorPopUpIsOpen = false;
        }

        private bool CanCloseConfirmationPopUp(object obj) => true;

        private void CloseConfirmationPopUp(object obj)
        {
            ConfirmationPopUpIsOpen = false;
        }

        private bool CanCloseSuccessPopUp(object obj) => true;

        private void CloseSuccessPopUp(object obj)
        {
            SuccessPopUpIsOpen = false;
        }

        private bool CanLogout(object obj) => true;

        private void Logout(object obj)
        {
            // Navigates to logout view
        }

        //Data grid commands
        private bool CanAddUnit(object obj) => true;

        private void AddUnit(object obj)
        {
            if (obj is Product product)
            {
                var dbProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                if(dbProduct != null)
                {
                    dbProduct.Units++;
                    context.SaveChanges();
                    RefreshProductList();
                }
                else
                {
                    ErrorPopUpMessage = "Error adding unit: Product not found.";
                    ErrorPopUpIsOpen = true;
                }
            }
        }

        private bool CanRemoveUnit(object obj)
        {
            if(obj is Product product)
            {
                return product.Units > 0;
            }
            return false;
        }

        private void RemoveUnit(object obj)
        {
            if (obj is Product product)
            {
                var dbProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                if (dbProduct != null && dbProduct.Units > 0)
                {
                    dbProduct.Units--;
                    context.SaveChanges();
                    RefreshProductList();
                }
                else
                {
                    ErrorPopUpMessage = "Error removing unit: Product not found or no units left.";
                    ErrorPopUpIsOpen = true;
                }
            }
        }

        private bool CanEditProductObj(object obj) => true;

        private void EditProductObj(object obj)
        {
            // Navigate to EditProductView
        }

        private bool CanDeleteProduct(object obj) => true;

        private void DeleteProduct(object obj)
        {
            if (productToDelete != null)
            {
                ConfirmationPopUpIsOpen = false;
                try
                {
                    var dbProduct = context.Products.FirstOrDefault(p => p.Id == productToDelete.Id);
                    if(dbProduct != null)
                    {
                        context.Products.Remove(dbProduct);
                        context.SaveChanges();
                        SuccessPopUpMessage = "Product deleted successfully.";
                        SuccessPopUpIsOpen = true;
                        RefreshProductList();
                    }
                    else
                    {
                        ErrorPopUpMessage = "Error deleting product: Product not found.";
                        ErrorPopUpIsOpen = true;
                    }
                }
                catch (Exception e)
                {
                    ErrorPopUpMessage = $"Error deleting product: {e.Message}";
                    ErrorPopUpIsOpen = true;
                }
                finally
                {
                    productToDelete = null;
                }
            }
        }

        private void RefreshProductList()
        {
            var products = context.Products.ToList();

            AllProducts = new ObservableCollection<Product>(products);
            FilteredProducts = new ObservableCollection<Product>(AllProducts);

            PagedProducts.Clear();
            foreach (var product in AllProducts.Take(5))
            {
                PagedProducts.Add(product);
            }

            CurrentPage = 1;
            UpdatePagedProducts();

            OnPropertyChanged(nameof(AllProducts));
            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(PagedProducts));
            OnPropertyChanged(nameof(NumPageItems));
        }



    }
}
