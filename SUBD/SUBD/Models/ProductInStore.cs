using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
	public class ProductInStore
	{
		public int Id { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int StoreId { get; set; }
		[Required]
		public int Count { get; set; }
		public virtual Product Product { get; set; } = new();
		public virtual Store Store { get; set; } = new();
	}
}
