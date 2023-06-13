using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
	[Table("Products")]
	public class Product
	{
		[Key]
		public int ProductId { set; get; }

		[Required]
		public string Name { set; get; }

		public string Provider { set; get; }
	}
}
