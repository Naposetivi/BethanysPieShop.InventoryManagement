using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductMagament
{
    public class Product
    {

        public Product(int id, string name, string? description, UnitType unitType, int maxAmountInStock, Price price)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;
            Price = price;

            AmountInStock = maxAmountInStock;

            UpdateLowStock();
        }

        public static int StockThreshold = 5;

        public static void ChangeStockThreshold(int newStockThreshold)
        {
            // we will only allow this to go through if the value is > 0
            if (newStockThreshold < 0) { 
                StockThreshold = newStockThreshold;
            }
        }

        #region Fields
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }
        public string? Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }
        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool IsBelowStockThreshold { get; private set; }
        public Price Price { get; set; }
        #endregion 
        //Using products
        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
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

        //Adding product in stock
        public void IncreaseStock()
        {
            AmountInStock++;
        }


        //Adding products in stock
        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock; // we only store the possible items, overstock isn`t stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn`t be stored.");
            }

            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }

        private void DecreiseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }
            UpdateLowStock();
            Log(reason);
        }

        //Displaying details of products
        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock.";
        }

        public string DisplayDetailsFull()
        {
            //StringBuilder sb = new();

            //sb.Append($"{id} {name} \n{description}\n{AmountInStock} item(s) in stock.");

            //if (IsBelowStockThreshold)
            //{
            //    sb.Append("\n!!STOCK LOW!!");
            //}

            //return sb.ToString();

            return DisplayDetailsFull("");
        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();

            sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock.");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        //Updating products if their number is too small
        private void UpdateLowStock()
        {
            if (AmountInStock < StockThreshold)//for now a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        //Changeing Console.WriteLine on Log method
        private void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        //Presentation of product
        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }
    }
}
