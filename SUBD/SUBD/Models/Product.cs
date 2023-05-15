using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUBD.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[ForeignKey("ProductId")]
		public virtual List<ProductInStore> ProductsInStore { get; set; } = new();
	}
}
