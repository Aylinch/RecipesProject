using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data.Account;
using RecipesProject.Models;

namespace RecipesProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    }
}
