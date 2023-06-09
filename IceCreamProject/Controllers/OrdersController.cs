using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Data;
using IceCreamProject.Models;

namespace IceCreamProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly RegDbContext _context;

        public OrdersController(RegDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            // code that shows admin the list of orders
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'RegDbContext.Orders'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // code that shows the admin, details of orders
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int id)
        {
            // code that takes user to Order create page where user can submit order
            var prod = _context.Book.FirstOrDefault(m => m.ID == id);
            ViewBag.pr_name=prod.B_name;
            ViewBag.pr_price = prod.Price;
            ViewBag.pr_id = id;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order_ID,Product_ID,Product_Name,Customer_Name,Email,Contact,Amount_Payable,Quantity")] Order order)
        {
            // code that allows user to submit an order
            if (ModelState.IsValid)
            {
                order.Amount_Payable = order.Amount_Payable * order.Quantity;

                
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Confirmation", "Orders", new
                {
                    otherParam = order.Order_ID,
                    anotherParam = order.Amount_Payable
                });
                
            }
            return View(order);

        }
        public IActionResult Confirmation(string otherParam, string anotherParam)
        {
            // code which shows user a confirmation message along with order id and amount payable
            ViewBag.id = otherParam;
            ViewBag.Amount = anotherParam;
            return View();

        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // code that takes the Admin to the edit page of an order
           
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Order_ID,Product_ID,Product_Name,Customer_Name,Email,Contact,Amount_Payable,Quantity")] Order order)
        {
            // code that allows admin to edit order details upon customer request
            if (id != order.Order_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Order_ID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // code that helps the admin to delete old order details or cancelled orders from the database
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'RegDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Order_ID == id)).GetValueOrDefault();
        }
    }
}
