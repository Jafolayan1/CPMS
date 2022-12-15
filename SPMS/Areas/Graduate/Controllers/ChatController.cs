using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Graduate.Controllers
{
    public class ChatController : BaseController
    {

        public ChatController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail) : base(userAccessor, context, mail)
        {
        }

        public IActionResult Index()
        {
            ViewData["Noti"] = GetNoti();
            return View();
        }
    }
}