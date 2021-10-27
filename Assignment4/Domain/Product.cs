using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Assignment4.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public string CategoryName { get { return Category.Name; } }
        public string ProductName { get { return Name; } }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
