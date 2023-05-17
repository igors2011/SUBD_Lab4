using Microsoft.EntityFrameworkCore;

namespace SUBD
{
	public static class DbWorker
	{
		public static void GetStores()
		{
			using Context context = new();
			var stores = context.Stores.ToList();
			if (stores.Count == 0)
			{
				Console.WriteLine("Список магазинов пуст");
			}
			else
			{
				foreach (var store in stores)
				{
					Console.WriteLine($"{store.Id}. Название: {store.Name}. Адрес: {store.Address}");
				}
			}
		}
		public static void GetProducts()
		{
			using Context context = new();
			var products = context.Products.ToList();
			if (products.Count == 0)
			{
				Console.WriteLine("Список продуктов пуст");
			}
			else
			{
				foreach (var Product in products)
				{
					Console.WriteLine($"{Product.Id}. Название: {Product.Name}. Цена: {Product.Price}");
				}
			}
		}
		public static void GetProductsInStores()
		{
			using Context context = new();
			var productsInStore = context.ProductsInStore
				.Include(x => x.Product)
				.Include(x => x.Store)
				.ToList();
			if (productsInStore.Count == 0)
			{
				Console.WriteLine("Ни в одном магазине нет товаров");
			}
			else
			{
				foreach (var productInStore in productsInStore)
				{
					Console.WriteLine($"{productInStore.Id}. Магазин № {productInStore.Store.Id}: {productInStore.Store.Name}. Продукт № {productInStore.Product.Id}: {productInStore.Product.Name}. Количество: {productInStore.Count}");
				}
			}
		}
		public static void GetSales()
		{
			using Context context = new();
			var sales = context.Sales
				.Include(x => x.ProductInStore)
				.ThenInclude(x => x.Store)
				.Include(x => x.ProductInStore)
				.ThenInclude(x => x.Product)
				.ToList();
			if (sales.Count == 0)
			{
				Console.WriteLine("Список продаж пуст");
			}
			else
			{
				foreach (var sale in sales)
				{
					Console.WriteLine($"Продажа № {sale.Id}. Продано {sale.Count} единиц продукта {sale.ProductInStore.Product.Name} в магазине {sale.ProductInStore.Store.Name}");
				}
			}
		}
	}
}
