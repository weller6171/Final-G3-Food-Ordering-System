using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalG3FoodOrderingSystem;
using FinalG3FoodOrderingSystem.Models;

namespace FinalG3FoodOrderingSystem.Controllers
{
    public class ShopsController : Controller
    {
        private FoodOrderingSystemDatabaseEntities7 db = new FoodOrderingSystemDatabaseEntities7();

        // GET: ProductDetails2
        public ActionResult Index()
        {
            var shopDetail = db.Shops.Include(p => p.User);
            return View(shopDetail.ToList());
        }

        // GET: ProductDetails2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop newShop = db.Shops.Find(id);
            if (newShop == null)
            {
                return HttpNotFound();
            }
            return View(newShop);
        }

        // GET: ProductDetails2/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ShopOwner = new SelectList(db.Users.Where(o => o.position == "Shop Owner"), "Id", "username");
            return View();
        }

        // POST: ProductDetails2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ShopName,ShopOwner,ShopAddress")] Shop newShop)
        {
            if (ModelState.IsValid)
            {
                db.Shops.Add(newShop);
                db.SaveChanges();
                return RedirectToAction("ShowShops", "Users", new { id = newShop.ShopOwner });
            }
            ViewBag.ShopOwner = new SelectList(db.Users.Where(o => o.position == "Shop Owner"), "Id", "username");
            return View(newShop);
        }

        // GET: ProductDetails2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop= db.Shops.Find(id);
            if (shop== null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopOwner = new SelectList(db.Users, "Id", "username", shop.ShopOwner);
            return View(shop);
        }


        public ViewResult ShowShops(int id)
        {
            return View(
                db.Shops.Where(r => r.ShopOwner == id).ToList());
        }

        public ViewResult CustomerShops()
        {
            return View(db.Shops.ToList());
        }

        // POST: ProductDetails2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ShopName,ShopOwner,ShopAddress")] Shop newShop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newShop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopOwner = new SelectList(db.Users, "Id", "username", newShop.ShopOwner);
            return View(newShop);
        }

        // GET: ProductDetails2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop newshop = db.Shops.Find(id);
            if (newshop == null)
            {
                return HttpNotFound();
            }
            return View(newshop);
        }

        // POST: ProductDetails2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shop shop = db.Shops.Find(id);
            db.Shops.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("Index");
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
