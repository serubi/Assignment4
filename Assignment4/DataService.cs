using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public interface IDataService
    {
        // Category
        IList<Category> GetCategories();
        Category GetCategory(int Id);
        Category CreateCategory(string Name, string Description);
        Category CreateCategory(Category category);
        bool DeleteCategory(int Id);
        bool UpdateCategory(int Id, string Name, string Description);

        // Product
        IList<Product> GetProducts();
        Product GetProduct(int Id);
        IList<Product> GetProductByCategory(int Id);
        IList<Product> GetProductByName(string SearchQuery);

        // Order
        Order GetOrder(int Id);
        IList<Order> GetOrders();

        // OrderDetails
        IList<OrderDetails> GetOrderDetailsByOrderId(int Id);
        IList<OrderDetails> GetOrderDetailsByProductId(int Id);
    }

    public class DataService : IDataService
    {
        public Category CreateCategory(string Name, string Description)
        {
            var category = new Category();
            var ctx = new NorthwindContext();
            category.Id = ctx.Categories.Max(x => x.Id) + 1;
            category.Name = Name;
            category.Description = Description;
            ctx.Add(category);
            ctx.SaveChanges();
            return category;
        }

        public Category CreateCategory(Category category)
        {
            var newCategory = new Category();
            var ctx = new NorthwindContext();
            newCategory.Id = ctx.Categories.Max(x => x.Id) + 1;
            newCategory.Name = category.Name;
            newCategory.Description = category.Description;
            ctx.Add(newCategory);
            ctx.SaveChanges();
            return newCategory;
        }

        public bool DeleteCategory(int Id)
        {
            var ctx = new NorthwindContext();
            var category = ctx.Categories.Find(Id);

            // Return false if the category is not found
            if (category == null) return false;

            ctx.Categories.Remove(category);
            return ctx.SaveChanges() > 0;
        }

        public IList<Category> GetCategories()
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.Find(Id);
        }

        public Order GetOrder(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }

        public IList<OrderDetails> GetOrderDetailsByOrderId(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Include(x => x.Product).ThenInclude(x => x.Category).Where(x => x.OrderId == Id).ToList();
        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Include(x => x.Product).ThenInclude(x => x.Category).Include(x => x.Order).Where(x => x.ProductId == Id).ToList();
        }

        public IList<Order> GetOrders()
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.ToList();
        }

        public Product GetProduct(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }

        public IList<Product> GetProductByCategory(int Id)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Include(x => x.Category).Where(x => x.CategoryId == Id).ToList();
        }

        public IList<Product> GetProductByName(string SearchQuery)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Include(x => x.Category).Where(x => x.Name.Contains(SearchQuery)).ToList();
        }

        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }

        public bool UpdateCategory(int Id, string Name, string Description)
        {
            var ctx = new NorthwindContext();
            var category = ctx.Categories.Find(Id);

            // Return false if the category is not found
            if (category == null) return false;

            category.Name = Name;
            category.Description = Description;
            return ctx.SaveChanges() > 0;
        }
    }
}
