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
    public class Line_ItemController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Line_Item
        public ActionResult Index()
        {
            var line_Item = db.Line_Item.Include(l => l.Game).Include(l => l.Invoice);
            return View(line_Item.ToList());
        }

        // GET: Line_Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line_Item line_Item = db.Line_Item.Find(id);
            if (line_Item == null)
            {
                return HttpNotFound();
            }
            return View(line_Item);
        }

        // GET: Line_Item/Create
        public ActionResult Create()
        {
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name");
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id");
            return View();
        }

        // POST: Line_Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "line_item_id,invoice_id,game_id,quantity,price")] Line_Item line_Item)
        {
            if (ModelState.IsValid)
            {
                db.Line_Item.Add(line_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name", line_Item.game_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", line_Item.invoice_id);
            return View(line_Item);
        }

        // GET: Line_Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line_Item line_Item = db.Line_Item.Find(id);
            if (line_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name", line_Item.game_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", line_Item.invoice_id);
            return View(line_Item);
        }

        // POST: Line_Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "line_item_id,invoice_id,game_id,quantity,price")] Line_Item line_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(line_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.game_id = new SelectList(db.Games, "game_id", "game_name", line_Item.game_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", line_Item.invoice_id);
            return View(line_Item);
        }

        // GET: Line_Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line_Item line_Item = db.Line_Item.Find(id);
            if (line_Item == null)
            {
                return HttpNotFound();
            }
            return View(line_Item);
        }

        // POST: Line_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Line_Item line_Item = db.Line_Item.Find(id);
            db.Line_Item.Remove(line_Item);
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
