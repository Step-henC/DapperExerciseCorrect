
using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace DapperDap
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion 

            IDbConnection conn = new MySqlConnection(connString);
            #region Departments
            DapperDepartmentRepository repos = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hi! Here are the current Departments");
            Console.WriteLine("Press any key to see List");
            Console.ReadLine();
            ReadDepList(repos.GetAllDepartments());

           

            Console.WriteLine("Would you like to add a Department? (Yes/No)");

            string userAnswer = Console.ReadLine(); 
            if(userAnswer.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your Department?");
                userAnswer = Console.ReadLine();
                repos.InsertDepartment(userAnswer);
                ReadDepList(repos.GetAllDepartments());
            }
            #endregion

            var prodos = new DapperProductRepository(conn);
            Console.WriteLine("Let's look at Products here at BestBuy");
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadLine();
            
            ReadProdList(prodos.GetAllProducts());

            Console.WriteLine("Would you like to add any Products? (yes/no)");
            var userResp = Console.ReadLine();
            if (userResp.ToLower() == "yes" || userResp.ToLower() == "y")
            {   
                Console.WriteLine("Please Provide the Name, Price and CategoryID?");
                string newName = Console.ReadLine();
                double newPrice = Convert.ToDouble(Console.ReadLine());
                int newCatID = Convert.ToInt32(Console.ReadLine());
                prodos.CreateProduct(newName, newPrice, newCatID);
                ReadProdList(prodos.GetAllProducts());
            }
            Console.WriteLine("This Product List is getting long!");
            Console.WriteLine("Any products you may want to delete? (yes/no)");
            string deleteResponse = Console.ReadLine();
            if(deleteResponse.ToLower() == "yes" || deleteResponse == "y")
            {
                Console.WriteLine("Please Provide the ID of the Product you'd like to delete");
                int idToDelete = Convert.ToInt32(Console.ReadLine());
                prodos.DeleteProduct(idToDelete);
                ReadProdList(prodos.GetAllProducts());
            }
            Console.WriteLine("Now, do we need to update the price any products we have?");
            string updateResponse = Console.ReadLine();

            if(updateResponse.ToLower() == "yes" || updateResponse == "y")
            {
                Console.WriteLine("Provide the Product ID of the product you'd like to update, then the new Price");
                
                int prodID = Convert.ToInt32(Console.ReadLine());    
                double updPrice = Convert.ToDouble(Console.ReadLine()); 
                prodos.UpdateProduct(prodID, updPrice);
                ReadProdList(prodos.GetAllProducts());
            }
            
            Console.WriteLine("Enjoy your day");
        }
        private static void ReadDepList(IEnumerable<Department> depos)
        {
            foreach (var department in depos)
            {
                Console.WriteLine($"ID: {department.DepartmentID} Department Name: {department.Name}");
                Thread.Sleep(500);
            }
        }
        private static void ReadProdList(IEnumerable<Product> prodos)
        {
            foreach (var product in prodos)
            {
                Console.WriteLine($"ID: {product.ProductID} Product Name: {product.Name} Product Price: {product.Price}");
                
            }
        }

    }
}
