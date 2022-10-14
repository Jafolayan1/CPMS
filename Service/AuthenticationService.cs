using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User AuthenticateUser(string userName, string Password)
        {
            var result = _signInManager.PasswordSignInAsync(userName, Password, false, lockoutOnFailure: false).Result;

            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(userName).Result ?? _userManager.FindByEmailAsync(userName).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Role = roles.ToArray();

                return user;
            }

            return null;
        }

        public User GetUser(string userName)
        {
            return _userManager.FindByNameAsync(userName).Result;
        }

        public async Task<bool> Signout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}