using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class User : IdentityUser<int>
	{
		public string? FullName { get; set; }
		public override string? Email { get; set; }
		public override string? PhoneNumber { get; set; }
		public string? ImageUrl { get; set; }

		[NotMapped]
		public string[] Role { get; set; }
	}
}