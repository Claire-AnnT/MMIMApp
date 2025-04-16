using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    class ProductManager
    {
        public static ObservableCollection<Product> products = new ObservableCollection<Product>();

        public static ObservableCollection<Product> GetProducts()
        {
            return products;
        }

        public static void AddProduct(Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Name == product.Name && p.Brand == product.Brand);
            if (existingProduct != null)
            {
                throw new ArgumentException($"Product with name {product.Name} and brand {product.Brand} already exists.");
            }
            products.Add(product);
        }

        public static void DeleteProduct(int productID)
        {
            var product = products.FirstOrDefault(p => p.Id == productID);
            if(product == null)
            {
                throw new Exception($"Product with ID {productID} not found.");
            }
            products.Remove(product);

        }

        public static void UpdateProduct(int productID, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == productID);
            var existingProduct = products.FirstOrDefault(p => p.Name == updatedProduct.Name && p.Brand == updatedProduct.Brand);
            if (product == null)
            {
                throw new Exception($"Product with ID {productID} not found.");
            }
            else if (existingProduct == updatedProduct)
            {
                throw new ArgumentException($"Product with name {updatedProduct.Name} and brand {updatedProduct.Brand} already exists.");
            }
            else
            {
                product.Name = updatedProduct.Name;
                product.Brand = updatedProduct.Brand;
                product.Description = updatedProduct.Description;
                product.UnitPrice = updatedProduct.UnitPrice;
                product.Units = updatedProduct.Units;
                product.MinUnit = updatedProduct.MinUnit;
                product.CategoryId = updatedProduct.CategoryId;
            }
        }

        public static void DecreaseProductStock(int productID)
        {
            var product = products.FirstOrDefault(p => p.Id == productID);
            if (product == null)
            {
                throw new Exception($"Product with ID {productID} not found.");
            }
            else if (product.Units <= 0)
            {
                throw new Exception($"Product with ID {productID} is out of stock.");
            }
            else
            {
                product.Units--;
            }
        }

        public static void IncreaseProductStock(int productID)
        {
            var product = products.FirstOrDefault(p => p.Id == productID);
            if (product == null)
            {
                throw new Exception($"Product with ID {productID} not found.");
            }
            else
            {
                product.Units++;
            }
        }

    }
}
