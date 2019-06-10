/* ProfilesController.cs
 * Author: Ryan Pease, John Lambert
 * 
 * Controls all logic and views for the user profile.
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
    public class ProfilesController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: friend Profile
        public ActionResult Index(int? id)
        {
            string uname = this.User.Identity.Name;
            var friend_List = db.Friend_List.Where(f => f.user_id == id || f.friend_id == id);
            bool friend = friend_List.Any(f => f.User.username == uname || f.User1.username == uname);
            bool isUser = db.Users.Where(f => f.user_id == id).Any(f => f.username == uname);
            var profile = db.Users.Where(f => f.user_id == id);
            if (friend || isUser)
            {
                return View(profile.ToList());
            }
            else
            {
                return RedirectToAction("nonFriend", new { id });
            }
        }
        //GET: Profiles/nonFriend/5
        public ActionResult nonFriend(int? id)
        {
            var profile = db.Users.Where(f => f.user_id == id);
            return View(profile.ToList());
        }

        //GET: Profiles/MyIndex/5
        public ActionResult MyIndex(string uname)
        {
            int id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            return RedirectToAction("Index", new { id });
        }

        //GET: Profiles/Edit/5
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.return_id = id;
            return View(user);
        }

        //POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id, username, email, login_failures, first_name, last_name, phone, gender, birthdate, date_joined, is_employee, is_admin, is_member, is_inactive, is_locked_out, is_on_email_list, favorite_platform, favorite_category, notes")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("MyIndex", new { uname = this.User.Identity.Name});
        }
    }
}