using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Models
{
    public class ShoppingCartItem 
    {
        public Part Part { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public void AddPart(Part part, int quantity = 1)
        {
            var existingItem = Items.FirstOrDefault(i => i.Part.Id == part.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new ShoppingCartItem
                {
                    Part = part,
                    Quantity = quantity
                });
            }
        }

        public void RemovePart(int partId)
        {
            var item = Items.FirstOrDefault(i => i.Part.Id == partId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal GetTotal()
        {
            return Items.Sum(i => i.Part.Price * i.Quantity);
        }
    }
}
