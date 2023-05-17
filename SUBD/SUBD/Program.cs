namespace SUBD
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Начало работы");
			var command = Console.ReadLine();
			while (command != "Выход")
			{
				switch (command)
				{
					case "Список продуктов":
						DbWorker.GetProducts();
						break;
					case "Список продуктов в магазинах":
						DbWorker.GetProductsInStores();
						break;
					case "Список продаж":
						DbWorker.GetSales();
						break;
					case "Список магазинов":
						DbWorker.GetStores();
						break;
					case "Добавить продукт":
						DbWorker.AddProduct();
						break;
					case "Добавить продукт в магазин":
						DbWorker.AddProductInStore();
						break;
					case "Добавить магазин":
						DbWorker.AddStore();
						break;
					case "Добавить продажу":
						DbWorker.AddSale();
						break;
					case "Обновить продукт":
						DbWorker.UpdateProduct();
						break;
					case "Обновить магазин":
						DbWorker.UpdateStore();
						break;
					case "Удалить продукт":
						DbWorker.DeleteProduct();
						break;
					case "Удалить магазин":
						DbWorker.DeleteStore();
						break;
					case "ТестУдалить":
						DbWorker.Truncate();
						break;
					case "Тест":
						DbWorker.TestCreate();
						DbWorker.TestRead();
						DbWorker.TestUpdate();
						DbWorker.TestDelete();
						DbWorker.TestReadByCount();
						break;
					default:
						Console.WriteLine("Неверный ввод");
						break;
				}
				command = Console.ReadLine();
			}
		}
	}
}