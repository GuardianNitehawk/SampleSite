using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGameStore.Models;

namespace VideoGameStore.Controllers
{
    public class AddressesController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.Province);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "address_id,street_address,city,province_id,postal_code")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code", address.province_id);
            return View(address);
        }

        // GET: Addresses/CreateUserAddress
        public ActionResult CreateUserAddress()
        {
            //ViewBag.user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code");
            return View();
        }

        [Authorize(Roles ="Customer, Admin, Employee")]
        // POST: Addresses/CreateUserAddress
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserAddress([Bind(Include = "address_id,street_address,city,province_id,postal_code")] Address address)
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();

                User_Address userAddress = new User_Address();
                int address_id = db.Addresses.Max(a => a.address_id);
                userAddress.user_id = user_id;
                userAddress.address_id = address_id;
                db.User_Address.Add(userAddress);
                db.SaveChanges();
                return RedirectToAction("Checkout", "Cart");
            }
            
            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code", address.province_id);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code", address.province_id);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "address_id,street_address,city,province_id,postal_code")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.province_id = new SelectList(db.Provinces, "province_id", "province_code", address.province_id);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
