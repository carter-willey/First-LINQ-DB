using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
            Console.ReadLine();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count

            var users = _context.Users;
            Console.WriteLine(users.ToList().Count); 
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            var products = _context.Products;

            foreach (Product product in products)
            {
                if (product.Price > 150)
                {
                    Console.WriteLine($"Product name: {product.Name} Product price: {product.Price}");
                }
            }
            // Then print the name and price of each product from the above query to the console.

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            foreach (Product product in products)
            {
                if (product.Name.Contains('s'))
                {
                    Console.WriteLine(product.Name);
                }
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            // 10 / 15 / 2012 12:00:00 AM
            DateTime obj = new DateTime(2016, 1, 1);
            
            var users = _context.Users;

            foreach (User user in users)
            {
                int result = DateTime.Compare((DateTime)user.RegistrationDate, obj);
                if (result < 0)
                {
                    Console.WriteLine($"{user.Email}");
                    Console.WriteLine(user.RegistrationDate);
                }
            }
        }

        private void ProblemSix()
        {
            DateTime date2016 = new DateTime(2016, 12, 31);
            DateTime date2018 = new DateTime(2018, 1, 1);
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var usersSpecific = _context.Users.Where(user => (DateTime.Compare((DateTime)user.RegistrationDate, date2016)) > 0 && (DateTime.Compare((DateTime)user.RegistrationDate, date2018)) < 0);

            foreach (User user in usersSpecific)
            {
                Console.WriteLine(user.Email);
                Console.WriteLine(user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var afton_shopping_cart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Email == "afton@gmail.com");
            foreach (ShoppingCart product in afton_shopping_cart)
            {
                Console.WriteLine($"{product.Product.Name} {product.Product.Price} {product.Quantity}");
            }

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var oda_shopping_cart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Id == 1);
            decimal sum = 0;
            foreach (ShoppingCart product in oda_shopping_cart)
            {
                sum += product.Product.Price;
                
            }
            Console.WriteLine(sum);


        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employeeUsers = _context.UserRoles.Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.UserId); // 3, 4
            var shoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => employeeUsers.Contains(sc.UserId));

            foreach (var cart in shoppingCart)
            {
                Console.WriteLine($"Email: {cart.User.Email} Product Name: {cart.Product.Name} Product Price: {cart.Product.Price} Quantity: {cart.Quantity}");
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Radio",
                Description = "Best radio in town!",
                Price = 190
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u=> u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(pr => pr.Id == 8).Select(pr => pr.Id).SingleOrDefault();

            ShoppingCart newItem = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newItem);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(pr => pr.Id == 8).SingleOrDefault();
            product.Price = 2000;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private string BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password."
            Console.WriteLine("Type in your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Type in a password:");
            string password = Console.ReadLine();
            bool inThere = false;
            var all_users = _context.Users;
            foreach (User user in all_users)
            {
                if (user.Email == email && user.Password == password)
                {
                    inThere = true;
                    break;
                }
            }
            if (inThere)
            {
                Console.WriteLine("Signed In!");
            }

            else
            {
                Console.WriteLine("Invalid Email or Password.");
                return "Not in There";
            }
            return email;
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var shoppingCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).OrderBy(sc => sc.UserId);
            var users = _context.ShoppingCarts.Select(sc => sc.User).Distinct();
            List<User> userList = new List<User>();
            decimal total = 0;
            decimal grandTotal = 0;
            int index = 0;
            foreach (User user in users)
            {
                userList.Add(user);
            }
            ShoppingCart last = shoppingCart.Last();
            foreach(ShoppingCart item in shoppingCart)
            {
                if (item.UserId == userList.ElementAt(index).Id)
                {
                    total += item.Product.Price * (decimal)item.Quantity;
                    grandTotal += item.Product.Price * (decimal)item.Quantity;

                }
                if (item.UserId != userList.ElementAt(index).Id)
                {
                    Console.WriteLine($"{userList.ElementAt(index).Email} has ${total} in their cart");
                    total = 0;
                    index++;
                    if (item.UserId == userList.ElementAt(index).Id)
                    {
                        total += item.Product.Price * (decimal)item.Quantity;
                        grandTotal += item.Product.Price * (decimal)item.Quantity;
                        
                    }
                    if (item == last)
                    {
                        Console.WriteLine($"{userList.ElementAt(index).Email} has ${total} in their cart");
                    }

                }
                    
            }
            
            Console.WriteLine(grandTotal);
        }

        public string userEmail = "";
        // BIG ONE
        private void BonusThree()
        {
            userEmail = "";
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

            string response = BonusOne();
            while (response == "Not in There")
            {
                response = BonusOne();
            }
            if (response != "Not in There")
            {
                userEmail = response;
                Console.WriteLine("Here are all the items in your shopping cart:");
                SpecificUserShoppingCart(userEmail);
                Console.WriteLine("\n");
                Console.WriteLine("Here are all the products we have to offer:");
                ViewAllProducts();
                Console.WriteLine("\n");
                Console.WriteLine("Choose from one of our products above by picking the number");
                int product_id = int.Parse(Console.ReadLine());
                GetProductId(product_id);
            }
        }

        private void GetProductId(int product_id)
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console
            var shopping_cart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.Product.Id == product_id && sc.User.Email == userEmail);
            //if (!shopping_cart.Contains(product_id))
            //{ 
                
            //}
            
            foreach (ShoppingCart item in shopping_cart)
            {
                if (item.ProductId == product_id)
                {
                    item.Quantity += 1;
                    _context.ShoppingCarts.Update(item);
                }
            }
            _context.SaveChanges();
        }

        private void SpecificUserShoppingCart(string response)
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var shopping_cart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Email == response);
            foreach (ShoppingCart product in shopping_cart)
            {
                Console.WriteLine($"{product.Product.Name} {product.Product.Price} {product.Quantity}");
            }
        }
        private void ViewAllProducts()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var products = _context.Products;
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Id} {product.Name} {product.Price}");
            }
        }

        private void UpdateQuantity(int incomingUserId, int incomingProductId)
        {
            var product = _context.ShoppingCarts.Where(sc => sc.UserId == incomingUserId && sc.ProductId == incomingProductId).SingleOrDefault();
            product.Quantity += 1;
            
            _context.ShoppingCarts.Update(product);
            _context.SaveChanges();
        }

        private void CreateNewProductInCart()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Radio",
                Description = "Best radio in town!",
                Price = 190
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }
    }
}
