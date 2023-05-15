using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUBD.Models
{
	public class Store
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Address { get; set; } = string.Empty;
		[ForeignKey("StoreId")]
		public virtual List<ProductInStore> ProductsInStore { get; set; } = new();
	}
}
