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

        /** Get list of task by Id user logged if it exist.
        * 
        *          
        */
        public async Task<IActionResult> Index()
        {            
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId)) return Redirect("~/Identity/Account/Login");
            IQueryable<TodoList> items = context.ToDoList
                .Where(task => task.ID_USUARIO == userId)
                .OrderBy(task => task.ID);
            List<TodoList> todolist = await items.ToListAsync();
            return View(todolist);            
        }

        /** Go to Create View
         * 
         */
        [ValidateAntiForgeryToken]
        public IActionResult Create() => View();


        /** Set Task.
         * 
         * @item TodoList item that is going to be inserted.         
         */
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

        /** Go to Edit View
         * 
         * @id Int, Id of the task which is going to be show on the edit View        
         */
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
       
        /** Put Task.
         * 
         * @item TodoList item that is going to be updated.         
         */
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

        /** Delete task.
         * 
         * @id Int, Id of the task that is going to be deleated.         
         */
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

