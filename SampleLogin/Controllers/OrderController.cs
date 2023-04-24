
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleLogin.Areas.Identity.Data;
using SampleLogin.Data;
using SampleLogin.Models;
using System.Security.Claims;

namespace SampleLogin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AuthDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public OrderController(AuthDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Order> allOrders = _db.Orders;
            IEnumerable<Order> filteredOrders = allOrders.Where(o => o.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier) && o.Status == "Pending");

            return View(filteredOrders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order obj)
        {
            obj.UserId = (User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "").ToString();
            obj.RiderId = "Waiting";
            obj.Status = "Pending";
            obj.StoreName = "storeA";
            _db.Orders.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int? id)
        {
                if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                if (obj.Status == "Ongoing")
                {
                    return RedirectToAction("Fail");
                }
                else
                {
                    obj.RiderId = (User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                    obj.Status = "Ongoing";
                    _db.Orders.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
        }

        public IActionResult Finish(int? id)
            
        {
            lock(_db) 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                obj.Status = "Success";
                _db.Orders.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult RiderOrder(Order obj)
        {
            IEnumerable<Order> allOrders = _db.Orders;
            IEnumerable<Order> filteredOrders = allOrders.Where(o => o.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier) && o.Status == "Pending");

            return View(filteredOrders);
        }

        public IActionResult Status()

        {
            IEnumerable<Order> allOrders = _db.Orders;
            return View(allOrders);
        }

        public IActionResult Store()
        {
            IEnumerable<Store> allOrders = _db.Stores;
            return View(allOrders);
        }

        public IActionResult Fail()
        {
            return View();
        }

    }
}