using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Vector
{
    /*SQL  FOR CORRECT WORK MODIFY YOUR CONECTION STRING
     * 
       CREATE DATABASE MyShop
       use MyShop;


        CREATE TABLE Categories (
	        CategoryID INT PRIMARY KEY IDENTITY(1,1),
	        CategoryName NVARCHAR(50),
	        CDescription VARCHAR(255)
        )

        CREATE TABLE Suppliers(
	        SupplierID INT PRIMARY KEY IDENTITY(1,1),
	        SupplierName NVARCHAR(50),
	        City VARCHAR(50),
	        Country VARCHAR(50)
        )

        CREATE TABLE Products(
	        ProductID INT PRIMARY KEY IDENTITY(1,1),
	        ProductName NVARCHAR(50),
	        SupplierID INT,
	        CategoryID INT,
	        Price DECIMAL(18,4),
	        CONSTRAINT FK_SUPPLIER_PRODUCT FOREIGN KEY (SupplierID) REFERENCES  Suppliers(SupplierID),
	        CONSTRAINT FK_CATEGORY_PRODUCT FOREIGN KEY (CategoryID) REFERENCES  Categories(CategoryID)
        )







        INSERT Categories(CategoryName,CDescription)
        VALUES ('Beverages','Soft drinks, coffees, teas, beers, and ales'),
	           ('Condiments','Sweet and savory sauces, relishes, spreads, and seasonings'),
	           ('Confections','Desserts, candies, and sweet breads'),
	           ('Dairy Products','Cheeses'),
	           ('Grains/Cereals','Breads, crackers, pasta, and cereal')
        INSERT Suppliers(SupplierName,City,Country)
        VALUES ('Exotic Liquid','London','UK'),
	           ('New Orleans Cajun Delights','New Orleans','USA'),
	           ('Grandma Kelly’s Homestead','Ann Arbor','USA'),
	           ('Tokyo Traders','Tokyo','Japan'),
	           ('Cooperativa de Quesos ‘Las Cabras’','Oviedo','Spain')

        Insert Products(ProductName,SupplierID,CategoryID,Price)
        Values ('Chais',1,1,18.00),
	        ('Chang',1,1,19.00),
	        ('Aniseed Syrup',1,2,10.00),
	        ('Chef Anton’s Cajun Seasoning',2,2,22.00),
	        ('Chef Anton’s Gumbo Mix',2,2,21.35)
	


        select * from Products
        where ProductName Like 'C%'

        select * from Products
        where Price =(SELECT Min(Price)from products)

        select * from Suppliers
        where SupplierID in ( Select SupplierID from products
        where CategoryID in (Select CategoryID from categories 
        where CategoryName = 'Condiments'))

        INSERT Suppliers (SupplierName,City,Country)
        VALUES ('Norske Meierier','Lviv','Ukraine')

        INSERT Products(ProductName,SupplierID,CategoryID,Price)
        VALUES('Green tea',(Select SupplierID from Suppliers where SupplierName='Norske Meierier'),(Select CategoryID from categories where CategoryName = 'Beverages'),10.0)

        select * from Suppliers
        select * from Products
        select * from Categories
     * 
     */

    class Program
    {
        static List<Product> getProducts()
        {
            var context = new MyShopContext();
            var products = context.Products.ToList();
            return products;
        }
        static List<Supplier> getSuppliers()
        {
            var context = new MyShopContext();
            var suppliers = context.Suppliers.ToList();
            return suppliers;
        }
        static List<Category> getCategories()
        {
            var context = new MyShopContext();
            var categories = context.Categories.ToList();
            return categories;
        }

        static void Main(string[] args)
        {

            var products = getProducts();
            var suppliers = getSuppliers();
            var categories = getCategories();



            Console.WriteLine("Products");
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Suppliers");
            foreach (var item in suppliers)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Categories");
            foreach (var item in categories)
            {
                Console.WriteLine(item.ToString());
            }

            /*●	Select product with product name that begins with ‘C’*/
            Console.WriteLine("Select product with product name that begins with ‘C’");
            var prod = products.Where(c => c.ProductName[0] == 'C' || c.ProductName[0] == 'c');
            foreach (var item in prod)
            {
                Console.WriteLine(item.ToString());
            }

            /* ●Select product with the smallest price. */
            Console.WriteLine("Select product with the smallest price.");
            var cheapest = products.OrderBy(c => c.Price).FirstOrDefault();
            if(cheapest!=null)
            Console.WriteLine(cheapest.ToString());

            /*●	Select cost of all products from the USA*/
            Console.WriteLine("Select cost of all products from the USA");
            var suppliersId = suppliers.Where(a => a.Country == "USA").Select(a => a.SupplierID.ToString()).ToArray();
            var usaprices = products.Where(c => suppliersId.Contains(c.SupplierID.ToString())).Select(p=>p.Price);
            foreach (var item in usaprices)
            {
                Console.WriteLine(item);
            }

            /*●	Select suppliers that supply Condiments.*/
            Console.WriteLine("Select suppliers that supply Condiments");
            var categoryId = categories.Where(c => c.CategoryName == "Condiments").Select(a=>a.CategoryID).FirstOrDefault();
            var condimentsSupliersId = products.Where(a => a.CategoryID == categoryId).Select(p => p.SupplierID.ToString()).ToArray();
            var condimentsSupliers = suppliers.Where(s => condimentsSupliersId.Contains(s.SupplierID.ToString()));

            foreach (var item in condimentsSupliers)
            {
                Console.WriteLine(item);
            }

            /*●	Add to database new supplier with name: ‘Norske Meierier’, city: ‘Lviv’, country: ‘Ukraine’ which will supply new product with name: ‘Green tea’, price: 10, and related to category with name: ‘Beverages’.*/
            Console.WriteLine("●Add to database new supplier with name: ‘Norske Meierier’, city: ‘Lviv’, country: ‘Ukraine’ which will supply new product with name: ‘Green tea’, price: 10, and related to category with name: ‘Beverages’.");

            var context = new MyShopContext();
            Supplier newSup = context.Suppliers.Add(new Supplier { City = "Lviv", SupplierName = "Norske Meierier", Country = "Ukraine" });

            categoryId = categories.Where(c => c.CategoryName == "Beverages").Select(a => a.CategoryID).FirstOrDefault();

            Product newProd = context.Products.Add(new Product { CategoryID = categoryId, ProductName = "green tea", Price = 10, SupplierID = newSup.SupplierID });

            context.SaveChanges();

            Console.WriteLine("Suppliers");
            foreach (var item in getSuppliers())
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Products");
            foreach (var item in getProducts())
            {
                Console.WriteLine(item.ToString());
            }
            
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
