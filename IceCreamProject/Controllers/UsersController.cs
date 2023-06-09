using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Data;
using IceCreamProject.Models;

namespace IceCreamProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly RegDbContext _context;

        public UsersController(RegDbContext context)
        {
            _context = context;
        }
        public IActionResult Confirmation(string otherParam, string anotherParam)
        {
            // code that shows user id and amount payable after user registration
            ViewBag.id = otherParam;
            if(string.Equals(anotherParam,"Monthly")||string.Equals(anotherParam,"monthly"))
            {
                ViewBag.Amount = 15;

            }
            else if(string.Equals(anotherParam,"Yearly")||string.Equals(anotherParam,"yearly"))
            {
                ViewBag.Amount = 150;
            }
            
            return View();

        }
        public IActionResult Login()
        {
            // code that takes the user to the login page
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email,string password)
        {
            // Code that verifies the email and password correction and takes user to other page
        

                 string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                SqlConnection con = new SqlConnection(connectionString);
                string a = "Active";
                con.Open();
            SqlCommand cmd = new SqlCommand("Select * from [User] where Email ='" +email+ "' and Password='" +password+ "' and Active='"+a+"'", con);
            SqlDataReader sddr = cmd.ExecuteReader();
            if (sddr.Read())
            {
                return RedirectToAction("Index","Recipes");
            }
            else
            {
                return NotFound();
           }

           

            return View();

            

        }
       

        // GET: Users
        public async Task<IActionResult> Index()
        {
            // code that shows the users list to admin
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'RegDbContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // code that shows details of a user to admin
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            // code that takes the user to registration page
            
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Password,Email,active,Payment")] Users users)
        {
            // code that allows a user to register
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Confirmation", "Users", new
                {
                    otherParam = users.UserID,
                    anotherParam = users.Payment
                });
                
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           // code that takes  admin to the edit page to edit the user details
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var users = await _context.User.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Password,Email,active,Payment")] Users users)
        {
            // code that edits the user data in database
            if (id != users.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // code that enables admin to delete a user
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'RegDbContext.User'  is null.");
            }
            var users = await _context.User.FindAsync(id);
            if (users != null)
            {
                _context.User.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
