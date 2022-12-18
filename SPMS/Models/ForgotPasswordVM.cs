using System.ComponentModel.DataAnnotations;

namespace SPMS.Models
{
	public class ForgotPasswordVM
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}