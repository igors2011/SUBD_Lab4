using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUBD.Models
{
	public class Store
	{
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Название магазина
		/// </summary>
		[Required]
		public string Name { get; set; } = "[Неизвестный магазин]";
		/// <summary>
		/// Адрес
		/// </summary>
		[Required]
		public string Address { get; set; } = "[Неизвестный адрес]";
		[ForeignKey("StoreId")]
		public virtual List<ProductInStore> ProductsInStore { get; set; } = new();
	}
}
