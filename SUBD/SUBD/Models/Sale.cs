using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
	public class Sale
	{
		/// <summary>
		/// Номер продажи
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Дата продажи
		/// </summary>
		[Required]
		public DateTime Date { get; set; } = DateTime.Now;
		/// <summary>
		/// Количество проданных продуктов
		/// </summary>
		[Required]
		public int Count { get; set; }
		[Required]
		public int ProductInStoreId { get; set; }
		public virtual ProductInStore ProductInStore { get; set; } = new();
	}
}
