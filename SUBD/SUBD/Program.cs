namespace SUBD
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Начало работы");
			DbActions actions= new();
			var queryString = Console.ReadLine();
			while (queryString != "Выход")
			{
				queryString = Console.ReadLine();
				switch(queryString)
				{
					case "Добавить продукт":
						actions.AddProduct();
						break;
					case "Добавить магазин":
						actions.AddStore();
						break;
					case "Список продуктов":
						actions.GetProducts();
						break;
					case "Список магазинов":
						actions.GetStores();
						break;
					case "Добавить продукт в магазин":
						actions.AddProductInStore();
						break;
					case "Продукты в магазинах":
						actions.GetProductsInStores();
						break;
					case "Продукты в магазине":
						actions.GetProductsInStore();
						break;
					case "Изменить продукт":
						actions.UpdateProduct();
						break;
					case "Изменить магазин":
						actions.UpdateStore();
						break;
					case "Удалить продукт":
						actions.DeleteProduct();
						break;
					case "Удалить магазин":
						actions.DeleteStore();
						break;
					case "Тест":
						actions.TestTime();
						break;
					default:
						Console.WriteLine("Неверный ввод");
						break;
				}
			}
		}
	}
}