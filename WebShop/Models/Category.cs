using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
	[Table("Category")]
	public class Category
	{
		[Key]
		public int CategoryId { set; get; }

		[StringLength(100)]
		public string Name { set; get; }

		[Column(TypeName = "ntext")]                  // Cột (trường) kiểu ntext trong SQL Server
		public string Description { set; get; }
	}
}
