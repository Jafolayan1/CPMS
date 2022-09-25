using System.ComponentModel.DataAnnotations;

namespace CPMS.Models
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
