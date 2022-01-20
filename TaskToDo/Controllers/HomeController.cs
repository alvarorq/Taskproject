using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskToDo.Models;

namespace TaskToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
       
        public IActionResult Index()
        {
            //TODO mio
            return Redirect(this._signInManager.IsSignedIn(User) ? "~/ToDo" : "~/Identity/Account/Login");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}
