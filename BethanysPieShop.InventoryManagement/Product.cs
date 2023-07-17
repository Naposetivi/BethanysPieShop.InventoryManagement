using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement
{
    public class Product
    {
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;

            AmountInStock = maxAmountInStock;

            UpdateLowStock();
        }

        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        public int Id { get; set; }
        public string Name { get
            {
                return name;
            }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }
        public string? Description { 
            get {
                return description;
            } 
            set {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            } }
        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool IsBelowStockThreshold { get; private set; }

        //using products
        public void UseProduct(int items)
        {
            if (items <= AmountInStock) {
                // use the items
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount if stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} reguested.");
            }
        }

        //adding products in stock
        public void IncreaseStock()
        {
            AmountInStock++;
        }

        private void DecreiseStock(int items, string reason)
        {
            if (items <= AmountInStock) {  
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }
            UpdateLowStock();
            Log(reason);
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock.";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            //ToDo: add price too
            sb.Append($"{id} {name} \n{description}\n{AmountInStock} item(s) in stock.");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        //updating products if their number is too small
        private void UpdateLowStock()
        {
            if (AmountInStock < 10)//for now a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        //changeing Console.WriteLine on Log method
        private void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        //presentation of product
        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }
    }
}
