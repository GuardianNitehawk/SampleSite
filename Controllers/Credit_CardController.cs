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
    public class Credit_CardController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        [Authorize(Roles = "Admin, Employee")]
        // GET: Credit_Card
        public ActionResult Index()
        {
            var credit_Card = db.Credit_Card.Include(c => c.User);
            return View(credit_Card.ToList());
        }

        [Authorize(Roles = "Admin, Employee, Customer, Member")]
        public ActionResult UserCards()
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            var credit_Card = db.Credit_Card.Include(c => c.User).Where(c => c.user_id == user_id);
            return View(credit_Card.ToList());
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Credit_Card/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            return View(credit_Card);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Credit_Card/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }
        
        // GET: Credit_Card/CreateUserCreditCard
        public ActionResult CreateUserCreditCard()
        {
            ViewBag.user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            return View();
        }

        [Authorize(Roles = "Admin, Employee")]
        // POST: Credit_Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "credit_card_id,user_id,card_number,expiry_date,is_expired,is_flagged")] Credit_Card credit_Card)
        {
            if (ModelState.IsValid)
            {
                db.Credit_Card.Add(credit_Card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            return View(credit_Card);
        }

        // POST: Credit_Card/CreateUserCreditCard
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserCreditCard([Bind(Include = "credit_card_id,user_id,card_number,expiry_date,is_expired,is_flagged")] Credit_Card credit_Card)
        {
            if (ModelState.IsValid)
            {
                db.Credit_Card.Add(credit_Card);
                db.SaveChanges();
                return RedirectToAction("Checkout", "Cart");
            }

            ViewBag.user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            return View(credit_Card);
        }

        // GET: Credit_Card/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", credit_Card.user_id);
            return View(credit_Card);
        }

        // POST: Credit_Card/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "credit_card_id,user_id,card_number,expiry_date,is_expired,is_flagged")] Credit_Card credit_Card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit_Card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", credit_Card.user_id);
            return View(credit_Card);
        }

        // GET: Credit_Card/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            if (credit_Card == null)
            {
                return HttpNotFound();
            }
            return View(credit_Card);
        }

        // POST: Credit_Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credit_Card credit_Card = db.Credit_Card.Find(id);
            db.Credit_Card.Remove(credit_Card);
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
