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
    public class Store_Event_UserController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Store_Event_User
        public ActionResult Index()
        {
            var store_Event_User = db.Store_Event_User.Include(s => s.Store_Event).Include(s => s.User);
            return View(store_Event_User.ToList());
        }

        // GET: Store_Event_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event_User store_Event_User = db.Store_Event_User.Find(id);
            if (store_Event_User == null)
            {
                return HttpNotFound();
            }
            return View(store_Event_User);
        }

        // GET: Store_Event_User/Create
        public ActionResult Create()
        {
            ViewBag.store_event_id = new SelectList(db.Store_Event, "store_event_id", "store_event_name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        // POST: Store_Event_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_event_user_id,store_event_id,user_id")] Store_Event_User store_Event_User)
        {
            if (ModelState.IsValid)
            {
                db.Store_Event_User.Add(store_Event_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.store_event_id = new SelectList(db.Store_Event, "store_event_id", "store_event_name", store_Event_User.store_event_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", store_Event_User.user_id);
            return View(store_Event_User);
        }

        // GET: Store_Event_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event_User store_Event_User = db.Store_Event_User.Find(id);
            if (store_Event_User == null)
            {
                return HttpNotFound();
            }
            ViewBag.store_event_id = new SelectList(db.Store_Event, "store_event_id", "store_event_name", store_Event_User.store_event_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", store_Event_User.user_id);
            return View(store_Event_User);
        }

        // POST: Store_Event_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_event_user_id,store_event_id,user_id")] Store_Event_User store_Event_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store_Event_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.store_event_id = new SelectList(db.Store_Event, "store_event_id", "store_event_name", store_Event_User.store_event_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", store_Event_User.user_id);
            return View(store_Event_User);
        }

        // GET: Store_Event_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event_User store_Event_User = db.Store_Event_User.Find(id);
            if (store_Event_User == null)
            {
                return HttpNotFound();
            }
            return View(store_Event_User);
        }

        // POST: Store_Event_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store_Event_User store_Event_User = db.Store_Event_User.Find(id);
            db.Store_Event_User.Remove(store_Event_User);
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
