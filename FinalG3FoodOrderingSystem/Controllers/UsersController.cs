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
    public class UsersController : Controller
    {
        private FoodOrderingSystemDatabaseEntities7 db = new FoodOrderingSystemDatabaseEntities7();






        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(FinalG3FoodOrderingSystem.Models.User user)
        {
            using (FoodOrderingSystemDatabaseEntities7 database1Entities = new FoodOrderingSystemDatabaseEntities7())
            {
                var userDetails = database1Entities.Users.Where(x => x.emailAddress == user.emailAddress && x.password == user.password).FirstOrDefault();
                if (userDetails == null)
                {
                    return View("Login");
                }
                else
                {
                    Session["emailAddress"] = userDetails.emailAddress;
                    if (userDetails.position == "Shop Owner")
                        return RedirectToAction("ShowShops", "Shops", new { id = userDetails.Id });
                    else if (userDetails.position == "Customer")
                        return RedirectToAction("DisplayFoods", "Foods", new { id = userDetails.Id });
                    else if (userDetails.position == "Delivery Boy")
                        return RedirectToAction("DisplayOrders", "Orders");
                    else
                        return View("Login");
                }
            }
        }


        // GET: ProductDetails2/Create
        public ActionResult Create()
        {
            User newUser = new User();
            return View(newUser);
        }

        // POST: ProductDetails2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, password, name, emailAddress, hpno, position")] User user)
        {
            FoodOrderingSystemDatabaseEntities7 database1Entities = new FoodOrderingSystemDatabaseEntities7();
            var userDetails = database1Entities.Users.Where(x => x.emailAddress == user.emailAddress).FirstOrDefault();
            if (userDetails == null)
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
            }
            else
            {
                Console.WriteLine("Email-Address already signed up before.");
            }
            return View(user);
        }






















        // GET: ShopDetails2
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ShopDetails2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: ShopDetails2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: ShopDetails2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,password,name,emailAddress,hpno,position")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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