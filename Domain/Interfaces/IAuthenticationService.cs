using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Signout();

        User AuthenticateUser(string UserName, string Password);

        User GetUser(string UserName);
    }
}