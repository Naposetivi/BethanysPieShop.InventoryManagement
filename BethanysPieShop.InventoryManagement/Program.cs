using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductMagament;

Product.ChangeStockThreshold(10);

Price samplePrice = new(10, Currency.Euro);
Product p1 = new(1, "Sugar", "Lorem ipsum", UnitType.PerKg, 100, samplePrice);

p1.IncreaseStock(10);
p1.Description = "Sample description";