using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeAndmebaas
{
    public class CartItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CartItem(string name, decimal price)
        {
            Name = name;
            Price = price;
            Quantity = 1;
        }
    }

}
