using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDap
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(string name, double price, int categoryID);

        void UpdateProduct(int productID, double price);

        void DeleteProduct(int prodID);
    }
}
