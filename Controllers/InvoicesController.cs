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
    public class InvoicesController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        [Authorize(Roles = "Admin, Employee")]
        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Credit_Card).Include(i => i.User);
            return View(invoices.ToList());
        }

        [Authorize(Roles = "Customer, Admin, Employee, Member")]
        public ActionResult DisplayUserInvoices()
        {
            int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
            var invoices = db.Invoices.Include(i => i.Credit_Card).Include(i => i.User).Where(i => i.user_id == user_id);
            return View(invoices.ToList());
        }

        [Authorize(Roles = "Customer, Admin, Employee, Member")]
        public ActionResult DisplayUserInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Invoice invoice = db.Invoices.Where(i => i.invoice_id == id).FirstOrDefault();
                int user_id = db.Users.Where(u => u.username == this.User.Identity.Name).FirstOrDefault().user_id;
                if (invoice.user_id != user_id)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     
                }
                else
                {
                    User user = db.Users.Where(u => u.user_id == user_id).FirstOrDefault();
                    int address_id = db.Invoice_Address.Where(a => a.invoice_id == invoice.invoice_id).FirstOrDefault().address_id;
                    Address address = db.Addresses.Where(a => a.address_id == address_id).FirstOrDefault();
                    Credit_Card credit_card = db.Credit_Card.Where(c => c.credit_card_id == invoice.credit_card_id).FirstOrDefault();
                    IEnumerable<Line_Item> items = db.Line_Item.Where(l => l.invoice_id == id).Include(l => l.Game).ToList();
                    return View(new UserInvoiceViewModel { invoice = invoice, user = user, address = address, credit_card = credit_card, items = items });
                }
            }                       
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.credit_card_id = new SelectList(db.Credit_Card, "credit_card_id", "credit_card_id");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        [Authorize(Roles = "Admin, Employee")]
        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoice_id,user_id,credit_card_id,invoice_date,ship_date,is_invoice_closed")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.credit_card_id = new SelectList(db.Credit_Card, "credit_card_id", "credit_card_id", invoice.credit_card_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", invoice.user_id);
            return View(invoice);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.credit_card_id = new SelectList(db.Credit_Card, "credit_card_id", "credit_card_id", invoice.credit_card_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", invoice.user_id);
            return View(invoice);
        }

        [Authorize(Roles = "Admin, Employee")]
        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoice_id,user_id,credit_card_id,invoice_date,ship_date,is_invoice_closed")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.credit_card_id = new SelectList(db.Credit_Card, "credit_card_id", "credit_card_id", invoice.credit_card_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", invoice.user_id);
            return View(invoice);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [Authorize(Roles = "Admin, Employee")]
        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
