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
					default:
						Console.WriteLine("Неверный ввод");
						break;
				}
				command = Console.ReadLine();
			}
		}
	}
}