using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SPMS.Models;

using System.Diagnostics;

namespace SPMS.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IUnitOfWork _context;
		private readonly UserManager<User> _userManager;

		public HomeController(IUserAccessor o, IUnitOfWork context, UserManager<User> userManager) : base(o)
		{
			_context = context;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Create(Message model)
		{
			if (ModelState.IsValid)
			{
				model.Username = CurrentUser.UserName;
				var sender = _userManager.GetUserAsync(User);
				model.UserId = sender.Id;
				_context.Messages.Add(model);
				await _context.SaveAsync();
				return Ok();
			}
			return Error();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}