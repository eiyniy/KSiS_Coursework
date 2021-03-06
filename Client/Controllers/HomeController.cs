using Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            ClientModel.Connect(11000);
            ViewBag.msg = ClientModel.SendMessageToServer("HELLLLLOOOOOO");
            ClientModel.Disconnect();
            return View();
        }
    }
}
