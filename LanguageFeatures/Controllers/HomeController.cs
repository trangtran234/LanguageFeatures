﻿using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ActionResult AutoProperty()
        {
            // Create a new Product object
            Product myProduct = new Product();

            // Set the property value
            myProduct.Name = "Kayak";

            //generate a view
            return View("Result", (object)String.Format("Product name: {0}", myProduct.Name));
        }

        public ActionResult CreateProduct()
        {
            // Create and populate a new Product object
            Product myProduct = new Product {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };

            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ActionResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                { "apple", 10 }, { "orange", 20 }, { "plum", 30 }
            };

            return View("Result", (object)stringArray[1]);
        }

        public ActionResult UseExtension()
        {
            // Create and populate ShoppingCart
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };

            // Get the total value of the products in the cart
            decimal cartTotal = cart.TotalPrices();

            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ActionResult UseExtensionEnum()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };

            // Create and populate an array of Product object
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            // Get the total value of the product in the cart
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Result", (object)String.Format("Cart Total: {0:c}, Array Total: {1:c}", cartTotal, arrayTotal));
        }

        public ActionResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                     new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                     new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                     new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                     new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                }
            };

            decimal total = 0;

            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer" || prod.Price > 20))
            {
                total += prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0:c}", total));
        }

        public ActionResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Pattern" },
                new { Name = "Hat", Category = "Clothing" },
                new { Name = "Apple", Category = "Fruit" }
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }

            return View("Result", (object)result.ToString());
        }

        public ActionResult FindProducts()
        {
            Product[] products =
            {
                 new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                 new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                 new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                 new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };

            // Awsome LINQ: dot notation
            var foundProducts = products.OrderByDescending(e => e.Price).Take(3).Select(e => new { e.Name, e.Price });

            products[2] = new Product { Name = "Stadium", Price = 79600M };

            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0:c} ", p.Price);
            }

            return View("Result", (object)result.ToString());
        }

        public ActionResult SumProducts()
        {
            Product[] products =
            {
                 new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                 new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                 new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                 new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };

            var results = products.Sum(e => e.Price);

            return View("Result", (object)String.Format("Sum: {0:c}", results));
        }
    }
}