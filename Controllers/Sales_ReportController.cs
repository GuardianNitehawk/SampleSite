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
    public class Sales_ReportController : Controller
    {
        private VideoGameStoreDBContext db = new VideoGameStoreDBContext();

        // GET: Sales_Report
        public ActionResult Index()
        {
            return View(db.Sales_Report.ToList());
        }

        // GET: Sales_Report/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Report sales_Report = db.Sales_Report.Find(id);
            if (sales_Report == null)
            {
                return HttpNotFound();
            }
            return View(sales_Report);
        }

        // GET: Sales_Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sales_Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "game_name,Units_Sold,Profit_Margin,Sales_Volume,Total_Profit")] Sales_Report sales_Report)
        {
            if (ModelState.IsValid)
            {
                db.Sales_Report.Add(sales_Report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sales_Report);
        }

        // GET: Sales_Report/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Report sales_Report = db.Sales_Report.Find(id);
            if (sales_Report == null)
            {
                return HttpNotFound();
            }
            return View(sales_Report);
        }

        // POST: Sales_Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "game_name,Units_Sold,Profit_Margin,Sales_Volume,Total_Profit")] Sales_Report sales_Report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_Report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sales_Report);
        }

        // GET: Sales_Report/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Report sales_Report = db.Sales_Report.Find(id);
            if (sales_Report == null)
            {
                return HttpNotFound();
            }
            return View(sales_Report);
        }

        // POST: Sales_Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sales_Report sales_Report = db.Sales_Report.Find(id);
            db.Sales_Report.Remove(sales_Report);
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
