using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMIMApp.Data;
using MMIMApp.Models;

namespace MMIMApp.Controllers
{
    public class ViewProductViewModel : ViewModelBase
    {
        private readonly MMIMAppContext context;

        private string productName =string.Empty;
        public string ProductName
        {
            get => productName;
        }

        private string brand = string.Empty;
        public string Brand
        {
            get => brand;
        }

        private string productCategory = string.Empty;
        public string ProductCategory
        {
            get => productCategory;
        }

        private string currentUnits = string.Empty;
        public string CurrentUnits
        {
            get => currentUnits;
        }

        private string unitPrice = string.Empty;
        public string UnitPrice
        {
            get => unitPrice;
        }

        private string description = string.Empty;
        public string Description
        {
            get => description;
        }

        public ViewProductViewModel(Product product)
        {
            context = new MMIMAppContext();

            var requestedProduct = context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (requestedProduct != null)
            {
                productName = requestedProduct.Name;
                brand = requestedProduct.Brand;
                productCategory = requestedProduct.Category?.Name ?? "No Category";
                currentUnits = requestedProduct.Units.ToString();
                unitPrice = requestedProduct.UnitPrice.ToString("C");
                description = requestedProduct.Description ?? "No Description";
            }
        }
    }
}
