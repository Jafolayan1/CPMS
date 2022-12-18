using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace SPMS.Areas.Graduate.Controllers
{
	public class ChatController : BaseController
	{
		public ChatController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail) : base(userAccessor, context, mail)
		{
		}

		[Route("chat")]
		public IActionResult Index()
		{
			ViewData["Noti"] = GetNoti();
			return View();
		}
	}
}