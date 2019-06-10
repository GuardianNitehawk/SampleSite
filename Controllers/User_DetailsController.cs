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
    public class User_DetailsController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: User_Details
        public ActionResult Index(int id)
        {
            int userID = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            if (id == userID)
            {
                var profile = db.Users.Where(f => f.user_id == id);
                return View(profile.ToList());
            }
            return RedirectToAction("Index", "Profiles", new { id = id });
        }

        // GET: User_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            int userID = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (id == userID)
            {
                var profile = db.Users.Where(f => f.user_id == id);
                return View(profile.ToList());
            }
            return RedirectToAction("Index", "Profiles", new { id = id });
        }

        // POST: User_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,username,email,user_password,login_failures,first_name,last_name,phone,gender,birthdate,date_joined,is_employee,is_admin,is_member,is_inactive,is_locked_out,is_on_email_list,favorite_platform,favorite_category,notes")] User user)
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
