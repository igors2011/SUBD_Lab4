using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUBD.Models
{
	public class ProductInStore
	{
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Количество
		/// </summary>
		[Required]
		public int Count { get; set; }
		/// <summary>
		/// Номер магазина
		/// </summary>
		[Required]
		public int StoreId { get; set; }
		/// <summary>
		/// Номер продукта
		/// </summary>
		[Required]
		public int ProductId { get; set; }
		public virtual Store Store { get; set; } = new();
		public virtual Product Product { get; set; } = new();
		[ForeignKey("ProductInStoreId")]
		public virtual List<Sale> Sales { get; set; } = new();
	}
}
