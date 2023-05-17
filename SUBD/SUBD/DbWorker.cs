using Microsoft.EntityFrameworkCore;
using SUBD.Models;
using System.Diagnostics;

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
					Console.WriteLine($"Продажа № {sale.Id}. Продано {sale.Count} единиц продукта {sale.ProductInStore.Product.Name} в магазине {sale.ProductInStore.Store.Name} на сумму {sale.Count * sale.ProductInStore.Product.Price}");
				}
			}
		}
		public static void AddProduct()
		{
			using Context context = new();
			Product product = new();
			Console.WriteLine("Название продукта?");
			product.Name = Console.ReadLine() ?? string.Empty;
			Console.WriteLine("Цена?");
			try
			{
				product.Price = Convert.ToDecimal(Console.ReadLine());
			}
			catch
			{
				product.Price = 0;
			}
			context.Products.Add(product);
			context.SaveChanges();
			Console.WriteLine("Продукт успешно добавлен");
		}
		public static void AddProductInStore()
		{
			using Context context = new();
			ProductInStore productInStore = new();
			try
			{
				Console.WriteLine("Номер продукта?");
				int productId = Convert.ToInt32(Console.ReadLine());
				productInStore.Product = context.Products.First(x => x.Id == productId);
				Console.WriteLine("Номер магазина?");
				int storeId = Convert.ToInt32(Console.ReadLine());
				productInStore.Store = context.Stores.First(x => x.Id == storeId);
				Console.WriteLine("Количество?");
				int count = Convert.ToInt32(Console.ReadLine());
				productInStore.Count = count;
				var existingProductInStore = context.ProductsInStore
					.Include(x => x.Product)
					.FirstOrDefault(x => x.ProductId == productId && x.StoreId == storeId);
				if (existingProductInStore != null)
				{
					existingProductInStore.Count += count;
					context.ProductsInStore.Update(existingProductInStore);
					context.SaveChanges();
					Console.WriteLine($"Увеличено количество продукта {existingProductInStore.Product.Name} в магазине");
				}
				else
				{
					context.ProductsInStore.Add(productInStore);
					context.SaveChanges();
					Console.WriteLine("Продукт успешно добавлен в магазин");
				}
			}
			catch
			{
				Console.WriteLine("Данные введены неверно");
			}
		}
		public static void AddStore()
		{
			using Context context = new();
			Store store = new();
			Console.WriteLine("Название магазина?");
			store.Name = Console.ReadLine() ?? string.Empty;
			Console.WriteLine("Адрес?");
			store.Address = Console.ReadLine() ?? string.Empty;
			context.Stores.Add(store);
			context.SaveChanges();
			Console.WriteLine("Магазин успешно добавлен");
		}
		public static void AddSale()
		{
			using Context context = new();
			Sale sale = new();
			try
			{
				Console.WriteLine("Количество?");
				sale.Count = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Магазин?");
				int storeNum = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Продукт");
				int productNum = Convert.ToInt32(Console.ReadLine());
				var existingProductInStore = context.ProductsInStore.FirstOrDefault(x => x.StoreId == storeNum && x.ProductId == productNum);
				if (existingProductInStore != null)
				{
					if (existingProductInStore.Count >= sale.Count)
					{
						existingProductInStore.Count -= sale.Count;
						context.Update(existingProductInStore);
						sale.ProductInStore = existingProductInStore;
						context.Sales.Add(sale);
						context.SaveChanges();
						Console.WriteLine("Продукт успешно продан");
					}
					else
					{
						Console.WriteLine("Нет нужного количества продукта в магазине!");
						return;
					}
				}
				else
				{
					Console.WriteLine("Такого продукта нет в магазине!");
					return;
				}
			}
			catch
			{
				Console.WriteLine("Данные введены неверно");
			}
		}
		public static void UpdateProduct()
		{
			try
			{
				Console.WriteLine("Номер продукта?");
				int productId = Convert.ToInt32(Console.ReadLine());
				using Context context = new();
				var product = context.Products.FirstOrDefault(x => x.Id == productId);
				if (product == null) 
				{
					Console.WriteLine("Не найден продукт с указанным номером");
					return;
				}
				Console.WriteLine("Новое название?");
				string productName = Console.ReadLine() ?? string.Empty;
				Console.WriteLine("Новая цена?");
				decimal price = Convert.ToDecimal(Console.ReadLine());
				product.Name = productName;
				product.Price = price;
				context.Products.Update(product);
				context.SaveChanges();
				Console.WriteLine("Информация о продукте успешно обновлена");
			}
			catch
			{
				Console.WriteLine("Неверный ввод");
			}
		}
		public static void UpdateStore()
		{
			try
			{
				Console.WriteLine("Номер магазина?");
				int storeId = Convert.ToInt32(Console.ReadLine());
				using Context context = new();
				var store = context.Stores.FirstOrDefault(x => x.Id == storeId);
				if (store == null)
				{
					Console.WriteLine("Не найден магазин с указанным номером");
					return;
				}
				Console.WriteLine("Новое название?");
				string storeName = Console.ReadLine() ?? string.Empty;
				Console.WriteLine("Новый адрес?");
				string address = Console.ReadLine() ?? string.Empty;
				store.Name = storeName;
				store.Address = address;
				context.Stores.Update(store);
				context.SaveChanges();
				Console.WriteLine("Информация о магазине успешно обновлена");
			}
			catch
			{
				Console.WriteLine("Неверный ввод");
			}
		}
		public static void DeleteProduct()
		{
			try
			{
				Console.WriteLine("Номер продукта?");
				int productId = Convert.ToInt32(Console.ReadLine());
				using Context context = new();
				var product = context.Products.FirstOrDefault(x => x.Id == productId);
				if (product == null)
				{
					Console.WriteLine("Не найден продукт с указанным номером");
					return;
				}
				context.Remove(product);
				context.SaveChanges();
				Console.WriteLine("Продукт успешно удалён");
			}
			catch
			{
				Console.WriteLine("Неверный ввод");
			}
		}
		public static void DeleteStore()
		{
			try
			{
				Console.WriteLine("Номер магазина?");
				int storeId = Convert.ToInt32(Console.ReadLine());
				using Context context = new();
				var store = context.Stores.FirstOrDefault(x => x.Id == storeId);
				if (store == null)
				{
					Console.WriteLine("Не найден магазин с указанным номером");
					return;
				}
				context.Remove(store);
				context.SaveChanges();
				Console.WriteLine("Магазин успешно удалён");
			}
			catch
			{
				Console.WriteLine("Неверный ввод");
			}
		}
		public static void Truncate()
		{
			using Context context = new();
			context.Sales.RemoveRange(context.Sales);
			context.ProductsInStore.RemoveRange(context.ProductsInStore);
			context.Products.RemoveRange(context.Products);
			context.Stores.RemoveRange(context.Stores);
			Console.WriteLine("База данных очищена");
		}
		//Для теста будем использовать сущность "Продукт в магазине"
		public static void TestCreate()
		{
			Random r = new();
			using Context context = new();
			var products = context.Products.ToList();
			var stores = context.Stores.ToList();
			List<ProductInStore> productInStores = new();
			for (int i = 0; i < 1000; i++)
			{
				var newProductInStore = new ProductInStore()
				{
					Product = products[r.Next(products.Count)],
					Store = stores[r.Next(stores.Count)],
					Count = r.Next(1000)
				};
				productInStores.Add(newProductInStore);
			}
			Stopwatch stopwatch = new();
			stopwatch.Start();
			context.ProductsInStore.AddRange(productInStores);
			context.SaveChanges();
			stopwatch.Stop();
			Console.WriteLine($"Вставка завершена. Времени затрачено: {stopwatch.Elapsed.TotalMilliseconds} мс");
		}
		public static void TestRead()
		{
			using Context context = new();
			Stopwatch stopwatch = new();
			stopwatch.Start();
			var products = context.Products.ToList();
			stopwatch.Stop();
			Console.WriteLine($"Чтение завершено. Времени затрачено: {stopwatch.Elapsed.TotalMilliseconds} мс");
		}
		public static void TestUpdate()
		{
			using Context context = new();
			Random r = new();
			List<ProductInStore> productInStores = context.ProductsInStore.ToList();
			foreach (var product in productInStores)
			{
				product.Count = r.Next(1000);
			}
			Stopwatch stopwatch = new();
			stopwatch.Start();
			context.ProductsInStore.UpdateRange(productInStores);
			context.SaveChanges();
			stopwatch.Stop();
			Console.WriteLine($"Обновление завершено. Времени затрачено: {stopwatch.Elapsed.TotalMilliseconds} мс");
		}
		public static void TestDelete()
		{
			using Context context = new();
			Random r = new();
			List<ProductInStore> productInStores = context.ProductsInStore.ToList();
			Stopwatch stopwatch = new();
			stopwatch.Start();
			context.ProductsInStore.RemoveRange(productInStores);
			context.SaveChanges();
			stopwatch.Stop();
			Console.WriteLine($"Удаление завершено. Времени затрачено: {stopwatch.Elapsed.TotalMilliseconds} мс");
		}
		public static void TestReadByCount()
		{
			using Context context = new();
			Random r = new();
			int Count = r.Next(1000);
			Stopwatch stopwatch = new();
			stopwatch.Start();
			var records = context.ProductsInStore.Where(x => x.Count == Count).ToList();
			stopwatch.Stop();
			Console.WriteLine($"Поиск по условию завершен. Времени затрачено: {stopwatch.Elapsed.TotalMilliseconds} мс");
		}
	}
}
