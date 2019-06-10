/*Friend_ListController.cs
 * Author: Ryan Pease, John Lambert
 * 
 * Controls the logic and views for the friend list pages.
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
    public class Friend_ListController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Friend_List
        public ActionResult Index(int? id)
        {
            var friend_List = db.Friend_List.Where(f => f.user_id == id || f.friend_id == id);
            ViewBag.return_id = id;
            return View(friend_List.ToList());
        }


        // GET: Friend_List/Create
        public ActionResult Create(int id)
        {
            int userID = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            ViewBag.friend_id = new SelectList(db.Users, "user_id", "username", id);
            ViewBag.user_id = new SelectList(db.Users.Where(f => f.username == this.User.Identity.Name), "user_id", "username", userID);
            ViewBag.return_id = id;
            return View();
        }

        // POST: Friend_List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,friend_id,is_family,date_added")] Friend_List friend_List)
        {
            if (ModelState.IsValid)
            {
                db.Friend_List.Add(friend_List);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = friend_List.user_id });
            }

            ViewBag.friend_id = new SelectList(db.Users, "user_id", "username", friend_List.friend_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", friend_List.user_id);
            return View(friend_List);
        }

        // GET: Friend_List/Delete/5
        public ActionResult Delete(int? uid, int? fid)
        {
            Friend_List friend_List = db.Friend_List.Where(f => f.user_id == uid && f.friend_id == fid).SingleOrDefault();
            if (friend_List == null)
            {
                return HttpNotFound();
            }
            ViewBag.return_id = db.Users.Where(f => f.username == this.User.Identity.Name).FirstOrDefault().user_id;
            return View(friend_List);
        }

        // POST: Friend_List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int uid, int fid)
        {
            Friend_List friend_List = db.Friend_List.Where(f => f.user_id == uid && f.friend_id == fid).SingleOrDefault();
            db.Friend_List.Remove(friend_List);
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