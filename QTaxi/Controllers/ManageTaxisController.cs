using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QTaxi.Models;

namespace QTaxi.Controllers
{
    public class ManageTaxisController : Controller
    {
        private TaxiEntities db = new TaxiEntities();

        // GET: ManageTaxis
        public ActionResult Index()
        {
            return View(db.Taxi.ToList());
        }

        // GET: ManageTaxis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxi.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // GET: ManageTaxis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageTaxis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,title,description,phone")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Taxi.Add(taxi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxi);
        }

        // GET: ManageTaxis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxi.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // POST: ManageTaxis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,title,description,phone")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxi);
        }

        // GET: ManageTaxis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxi.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // POST: ManageTaxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Taxi taxi = db.Taxi.Find(id);
            db.Taxi.Remove(taxi);
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
