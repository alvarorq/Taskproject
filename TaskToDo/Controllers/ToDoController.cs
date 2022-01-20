using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskToDo.Data;
using TaskToDo.Models;

namespace TaskToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext context;

        public ToDoController(ApplicationDbContext context,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.context = context;
        }
         
        public async Task<IActionResult> Index()
        {
            //TODO Linq
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId)) return Redirect("~/Identity/Account/Login");
            IQueryable<TodoList> items = context.ToDoList
                .Where(task => task.ID_USUARIO == userId)
                .OrderBy(task => task.ID);
            List<TodoList> todolist = await items.ToListAsync();
            return View(todolist);            
        }
        
        [ValidateAntiForgeryToken]
        public IActionResult Create() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            item.ID_USUARIO = userId;
            item.CREATED =  DateTime.Now;
            item.LAST_UPD =  DateTime.Now;
            

            if (item.ID_USUARIO != null )
            {
                context.Add(item);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            TodoList item = await context.ToDoList.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                item.LAST_UPD = DateTime.Now;
                context.Update(item);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(item);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TodoList item = await context.ToDoList.FindAsync(id);

            if (item != null)
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }
    }
}

