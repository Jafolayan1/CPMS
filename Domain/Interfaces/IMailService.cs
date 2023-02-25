using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IMailService
	{
		Task SendEmailAsync(MailRequest mailRequest, string body);
	}
}