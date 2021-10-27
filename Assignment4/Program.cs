using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataService = new DataService();

            // Debugging
            var result = dataService.GetOrderDetailsByProductId(11);

            Console.WriteLine(result);
            Console.WriteLine("-------");
            Console.WriteLine(result.Count);
            Console.WriteLine(result.First().Order.Date.ToString("yyyy-MM-dd"));
        }
    }
}
