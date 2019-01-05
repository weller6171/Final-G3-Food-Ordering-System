using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalG3FoodOrderingSystem.Models;

namespace FinalG3FoodOrderingSystem.Controllers
{
    public class OrdersController : Controller
    {
        private FoodOrderingSystemDatabaseEntities6 db = new FoodOrderingSystemDatabaseEntities6();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Food).Include(o => o.Shop).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "FoodName");
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "ShopName");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "username");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,FoodId,Quantity,TotalPrice,Status,ShopId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("ShowOrders", "Orders", new { id = order.CustomerId});
            }

            ViewBag.FoodId = new SelectList(db.Foods, "Id", "FoodName", order.FoodId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "ShopName", order.ShopId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "username", order.CustomerId);
            return View(order);
        }
        public ViewResult ShowOrders(int id)
        {
            return View(
                db.Orders.Where(r => r.ShopId == id).ToList());
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "FoodName", order.FoodId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "ShopName", order.ShopId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "username", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,FoodId,Quantity,TotalPrice,Status,ShopId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "FoodName", order.FoodId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "ShopName", order.ShopId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "username", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ReadytoServe(int? id)
        {
            Order newOrder = db.Orders.Find(id);
            if (newOrder == null)
            {
                return HttpNotFound();
            }
            else if (newOrder.Status == "Unpaid")
            {
                newOrder.Status = "Ready to Deliver";
                db.SaveChanges();
                return RedirectToAction("ShowOrders", "Orders",new{ id = newOrder.ShopId});
            }
            else if (newOrder.Status == "Ready to Deliver")
            {
                newOrder.Status = "Accepted to Deliver";
                db.SaveChanges();
                return RedirectToAction("DisplayOrders");
            }

            return RedirectToAction("DisplayOrders");
        }

        

        public ViewResult DisplayOrders()
        {
            return View(db.Orders.ToList());
        }













        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
