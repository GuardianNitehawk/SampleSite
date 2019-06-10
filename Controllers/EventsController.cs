/* File Name:
 * EventsController.cs
 * 
 * 
 */

using MySql.Data.MySqlClient;
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
    public class EventsController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        public ActionResult UserEventsIndex()
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            var store_Event_User = db.Store_Event_User.Where(f => f.user_id == user_id);
            //checkIfUserIsRegisteredToEvent();
            return View(store_Event_User.ToList());
        }

        public ActionResult Register(int store_event_id)
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;

            SharedDB.setConnectionString();
            SharedDB.command = new MySqlCommand("INSERT INTO Store_Event_User (store_event_id, user_id) VALUES (" + store_event_id + ", " + user_id + ")", SharedDB.connection);
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                SharedDB.command.ExecuteNonQuery();
            }

            return RedirectToAction("UserEventsIndex");
        }

        // GET: Events
        public ActionResult Index()
        {
            var store_Event = db.Store_Event.Include(s => s.Address);
            checkIfUserIsRegisteredToEvent();
            return View(store_Event.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event store_Event = db.Store_Event.Find(id);
            if (store_Event == null)
            {
                return HttpNotFound();
            }
            return View(store_Event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_event_id,store_event_name,description,address_id,start_date,end_date,max_registrants,is_full,is_members_only,is_cancelled")] Store_Event store_Event)
        {
            if (ModelState.IsValid)
            {
                db.Store_Event.Add(store_Event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", store_Event.address_id);
            return View(store_Event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event store_Event = db.Store_Event.Find(id);
            if (store_Event == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", store_Event.address_id);
            return View(store_Event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_event_id,store_event_name,description,address_id,start_date,end_date,max_registrants,is_full,is_members_only,is_cancelled")] Store_Event store_Event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store_Event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", store_Event.address_id);
            return View(store_Event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store_Event store_Event = db.Store_Event.Find(id);
            if (store_Event == null)
            {
                return HttpNotFound();
            }
            return View(store_Event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store_Event store_Event = db.Store_Event.Find(id);
            deleteEventRegistrations(id);
            db.Store_Event.Remove(store_Event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UnRegister(int store_event_id)
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;

            SharedDB.setConnectionString();

            SharedDB.command = new MySqlCommand("SELECT store_event_id FROM ", SharedDB.connection);


            SharedDB.command = new MySqlCommand("DELETE FROM Store_Event_User WHERE store_event_id = " + store_event_id + " AND user_id = " + 
                user_id, SharedDB.connection);
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                SharedDB.command.ExecuteNonQuery();
            }

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

        public string[] getEventIDS()
        {
            int num_of_events = 0;

            SharedDB.setConnectionString();
            SharedDB.command = new MySqlCommand("SELECT store_event_id FROM Store_Event ORDER BY store_event_id DESC LIMIT 1", SharedDB.connection);
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                MySqlDataReader reader = SharedDB.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        num_of_events = reader.GetInt32(0);
                    }
                }
            }

            string[] results = new string[num_of_events];
            int counter = 0;

            SharedDB.command = new MySqlCommand("SELECT store_event_id FROM Store_Event", SharedDB.connection);
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                MySqlDataReader reader = SharedDB.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results[counter] = reader.GetString(0);
                        counter++;
                    }
                }
            }

            return results;

        }

        public void deleteEventRegistrations(int id)
        {
            SharedDB.setConnectionString();
            SharedDB.command = new MySqlCommand("DELETE FROM Store_Event_User WHERE store_event_id = " + id, SharedDB.connection);
            SharedDB.connection.Open();
            using (SharedDB.connection)
            {
                SharedDB.command.ExecuteNonQuery();
            }
        }


        public void checkIfUserIsRegisteredToEvent()
        {
            string[] events = getEventIDS();
            string[] user_events = new string[events.Length];
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;

            SharedDB.setConnectionString();
            SharedDB.command = new MySqlCommand("SELECT store_event_id FROM Store_Event_User WHERE user_id = " + user_id, SharedDB.connection);
            SharedDB.connection.Open();

            using (SharedDB.connection)
            {
                MySqlDataReader reader = SharedDB.command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user_events[reader.GetInt32(0) - 1] = reader.GetString(0);
                    }
                }
            }
            ViewData["user_events"] = user_events;
        }
    }
}
