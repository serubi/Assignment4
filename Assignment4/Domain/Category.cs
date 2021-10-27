using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Assignment4.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
