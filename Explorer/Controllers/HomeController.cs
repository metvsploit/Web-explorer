using Explorer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService) => _homeService = homeService;

        public IActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        [Route("/home/get")]
        public async Task<IActionResult> GetFoldersAsync()
        {
            var response = await _homeService.GetTreeNodeAsync();

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
