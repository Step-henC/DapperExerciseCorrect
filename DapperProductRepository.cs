using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;

namespace DapperDap
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connct;

        public DapperProductRepository(IDbConnection connct )
        {
            _connct = connct;   
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connct.Execute("INSERT INTO products (Name, Price, ProductID) VALUES (@prodName, @prodPrice, @prodID);",
                new { prodName = name, prodPrice = price, prodID = categoryID });   
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connct.Query<Product>("SELECT * FROM products");

             
        }
        public void DeleteProduct(int productID)
        {
            _connct.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connct.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connct.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }
        public void UpdateProduct(int prodID, double updPrice)
        {
            _connct.Execute("UPDATE products SET Price = @newPrice WHERE ProductID = @input;",
                new { input = prodID, newPrice = updPrice});
        }
    }
}
