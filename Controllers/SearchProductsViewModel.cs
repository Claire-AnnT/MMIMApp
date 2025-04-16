using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MMIMApp.Commands;
using MMIMApp.Models;
using MMIMApp.Views.Controls;
using MMIMApp.Views.ProductViews;
using static MMIMApp.Commands.RelayCommand;

namespace MMIMApp.Controllers
{
    class SearchProductsViewModel : ViewModelBase
    {
        //Observable collections
        public ObservableCollection<Product> AllProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> PagedProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> FilterdProducts { get; private set; }

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

        //Commands
        public ICommand AddUnitCommand { get; }
        public ICommand RemoveUnitCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand CloseSuccessPopup { get; }
        public ICommand ShowConfirmatioPopUp { get; }
        public ICommand CloseConfirmationPopup { get; }
        public ICommand CloseErrorPopup { get; }


        //Constructor
        public SearchProductsViewModel()
        {
            //Initialize collections
            AllProducts = ProductManager.GetProducts();

            //Dummy data for testing
            User TestUser = new User("TestUser", "TestPassword", "TestFirstName", "TestLastName", false);

            for (int i = 0; i < 25; i++)
            {
                Product newProduct = new Product($"Product {i}",$"Brand {i}",(decimal)0.00,3,1,TestUser,null);
                AllProducts.Add(newProduct);
            }

            FilterdProducts = new ObservableCollection<Product>(AllProducts);
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
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            LogoutCommand = new RelayCommand(Logout, CanLogout);
            CloseSuccessPopup = new RelayCommand(CloseSuccessPopUp, CanCloseSuccessPopUp);
            CloseConfirmationPopup = new RelayCommand(CloseConfirmationPopUp, CanCloseConfirmationPopUp);
            CloseErrorPopup = new RelayCommand(CloseErrorPopUp, CanCloseErrorPopUp);
            ShowConfirmatioPopUp = new RelayCommand(ShowConfirmationPopUp, CanShowConfirmationPopUp);
        }

        private Product productToDelete;
        
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
                ProductManager.IncreaseProductStock(product.Id); 
                RefreshProductList(); 
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
                ProductManager.DecreaseProductStock(product.Id);
                RefreshProductList();
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
                    ProductManager.DeleteProduct(productToDelete.Id);
                    SuccessPopUpMessage = $"{productToDelete.Name} deleted successfully.";
                    SuccessPopUpIsOpen = true;
                    RefreshProductList();
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
            AllProducts = ProductManager.GetProducts();
            
            FilterdProducts = new ObservableCollection<Product>(AllProducts);
            PagedProducts.Clear();
            foreach (var product in AllProducts.Take(5))
            {
                PagedProducts.Add(product);
            }

            OnPropertyChanged(nameof(AllProducts));
            OnPropertyChanged(nameof(FilterdProducts));
            OnPropertyChanged(nameof(PagedProducts));
        }



    }
}
