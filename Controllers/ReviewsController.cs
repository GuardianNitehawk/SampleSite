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
    public class ReviewsController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Reviews
        public ActionResult Index(int? id)
        {
            if (id != null || id != 0)
            {
                var reviews = db.Reviews.Include(r => r.Game).Include(r => r.User).Where(r => r.game_id == id);
                return View(reviews.ToList());
            }
            else
            {
                return RedirectToAction("Index","Games");
            }
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int? userGameID)
        {
            Review review = new Review();

            if (userGameID != 0)
            {
                User_Game userGame = db.User_Game.Find(userGameID);
                ViewBag.rating = userGame.rating;
                ViewBag.rating = userGame.rating;

                Game game = db.Games.Find(userGame.game_id);

                User user = db.Users.Find(userGame.user_id);

                review.Game = game;
                review.User = user;
                review.user_id = user.user_id;
                review.game_id = game.game_id;
                review.review_date = DateTime.Now;
                review.review_content = "";
                review.is_approved = false;
                review.is_deleted = false;
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ViewBag.userGameID = userGameID;
            return View(review);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "review_id,user_id,game_id,review_date,review_content,is_approved,is_deleted")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id, int? userGameID, bool? approve, bool? delete)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            if (userGameID != 0)
            {
                User_Game userGame = db.User_Game.Find(userGameID);
                ViewBag.datePurchased = userGame.date_purchased;
                ViewBag.rating = userGame.rating;
            }
            ViewBag.userGameID = userGameID;


            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "review_id,user_id,game_id,review_date,review_content,is_approved,is_deleted")] Review review)
        {
            Review oldReveiw = db.Reviews.Find(review.review_id);
            if(review.review_content != oldReveiw.review_content)
            {
                review.is_approved = false;
            }
             
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
