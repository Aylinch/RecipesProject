using Microsoft.AspNetCore.Mvc;
using RecipesProject.Contracts;
using RecipesProject.Models;
using System.Diagnostics;

namespace RecipesProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService)
        {
            this.recipeService = recipeService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await this.recipeService.TodaySpacial();
            return View(model);
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