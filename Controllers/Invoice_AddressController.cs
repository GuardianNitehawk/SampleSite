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
    public class Invoice_AddressController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Invoice_Address
        public ActionResult Index()
        {
            var invoice_Address = db.Invoice_Address.Include(i => i.Address).Include(i => i.Invoice);
            return View(invoice_Address.ToList());
        }

        // GET: Invoice_Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Address invoice_Address = db.Invoice_Address.Find(id);
            if (invoice_Address == null)
            {
                return HttpNotFound();
            }
            return View(invoice_Address);
        }

        // GET: Invoice_Address/Create
        public ActionResult Create()
        {
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address");
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id");
            return View();
        }

        // POST: Invoice_Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoice_address_id,invoice_id,address_id,is_billing_address")] Invoice_Address invoice_Address)
        {
            if (ModelState.IsValid)
            {
                db.Invoice_Address.Add(invoice_Address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", invoice_Address.address_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", invoice_Address.invoice_id);
            return View(invoice_Address);
        }

        // GET: Invoice_Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Address invoice_Address = db.Invoice_Address.Find(id);
            if (invoice_Address == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", invoice_Address.address_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", invoice_Address.invoice_id);
            return View(invoice_Address);
        }

        // POST: Invoice_Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoice_address_id,invoice_id,address_id,is_billing_address")] Invoice_Address invoice_Address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_Address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "street_address", invoice_Address.address_id);
            ViewBag.invoice_id = new SelectList(db.Invoices, "invoice_id", "invoice_id", invoice_Address.invoice_id);
            return View(invoice_Address);
        }

        // GET: Invoice_Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Address invoice_Address = db.Invoice_Address.Find(id);
            if (invoice_Address == null)
            {
                return HttpNotFound();
            }
            return View(invoice_Address);
        }

        // POST: Invoice_Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice_Address invoice_Address = db.Invoice_Address.Find(id);
            db.Invoice_Address.Remove(invoice_Address);
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
