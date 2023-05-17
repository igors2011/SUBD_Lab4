using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUBD.Models
{
	public class Product
	{
		/// <summary>
		/// Номер продукта
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Название продукта
		/// </summary>
		[Required]
		public string Name { get; set; } = "[Неизвестный продукт]";
		/// <summary>
		/// Цена продукта
		/// </summary>
		[Required, Column(TypeName ="decimal(10,2)")]
		public decimal Price { get; set; }
		[ForeignKey("ProductId")]
		public virtual List<ProductInStore> ProductsInStore { get; set; } = new();
	}
}
