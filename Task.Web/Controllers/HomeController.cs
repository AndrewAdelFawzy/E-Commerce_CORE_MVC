using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Infrastructure.Presistence;
using Task.Web.Models;

namespace Task.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel
            {
                Products = await _context.Products
                             .Include(p => p.Brand)
                             .OrderBy(p => Guid.NewGuid())  
                             .Take(4)                      
                             .ToListAsync(),
                Brands = await _context.Brands
                             .OrderBy(p => Guid.NewGuid())  
                             .Take(3)
                             .ToListAsync()
            };
            return View(viewModel);
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
