/*Wish_ListController.cs
 * Author: Ryan Pease, John Lambert
 * 
 * Controls all logic and views for the wish list pages
 * 
 */
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
    public class Wish_ListController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Wish_List
        public ActionResult Index(int? id)
        {
            var wish_List = db.Wish_List.Where(f => f.user_id == id);
            ViewBag.return_id = id;
            return View(wish_List.ToList());
        }

        // GET: Wish_List/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wish_List wish_List = db.Wish_List.Find(id);
            if (wish_List == null)
            {
                return HttpNotFound();
            }
            return View(wish_List);
        }

        // GET: Wish_List/Create
        public ActionResult Create(int? id)
        {
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name");
            //need a way to pass in the user_id of current user 
            ViewBag.user_id = new SelectList(db.Users.Where(f => f.username == this.User.Identity.Name), "user_id", "username");
            return View();
        }

        // POST: Wish_List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "wish_list_id,user_id,game_id,date_added")] Wish_List wish_List)
        {
            if (ModelState.IsValid)
            {
                db.Wish_List.Add(wish_List);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = wish_List.user_id });
            }

            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name", wish_List.game_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", wish_List.user_id);
            return View(wish_List);
        }

        // GET: Wish_List/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wish_List wish_List = db.Wish_List.Find(id);
            if (wish_List == null)
            {
                return HttpNotFound();
            }
            ViewBag.return_id = wish_List.user_id;
            return View(wish_List);
        }

        // POST: Wish_List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wish_List wish_List = db.Wish_List.Find(id);
            db.Wish_List.Remove(wish_List);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = wish_List.user_id });
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