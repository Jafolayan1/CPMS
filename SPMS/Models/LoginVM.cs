using System.ComponentModel.DataAnnotations;

namespace CPMS.Models
{
	public class LoginVM
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}