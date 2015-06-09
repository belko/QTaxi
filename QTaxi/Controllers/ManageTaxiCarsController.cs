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
    public class ManageTaxiCarsController : Controller
    {
        private TaxiEntities db = new TaxiEntities();

        // GET: ManageTaxiCars
        public ActionResult Index()
        {
            var taxiCars = db.TaxiCars.Include(t => t.Car).Include(t => t.Taxi);
            return View(taxiCars.ToList());
        }

        // GET: ManageTaxiCars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiCars taxiCars = db.TaxiCars.Find(id);
            if (taxiCars == null)
            {
                return HttpNotFound();
            }
            return View(taxiCars);
        }

        // GET: ManageTaxiCars/Create
        public ActionResult Create()
        {
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark");
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title");
            return View();
        }

        // POST: ManageTaxiCars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,taxi_id,car_id")] TaxiCars taxiCars)
        {
            if (ModelState.IsValid)
            {
                db.TaxiCars.Add(taxiCars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", taxiCars.car_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", taxiCars.taxi_id);
            return View(taxiCars);
        }

        // GET: ManageTaxiCars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiCars taxiCars = db.TaxiCars.Find(id);
            if (taxiCars == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", taxiCars.car_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", taxiCars.taxi_id);
            return View(taxiCars);
        }

        // POST: ManageTaxiCars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,taxi_id,car_id")] TaxiCars taxiCars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxiCars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", taxiCars.car_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", taxiCars.taxi_id);
            return View(taxiCars);
        }

        // GET: ManageTaxiCars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiCars taxiCars = db.TaxiCars.Find(id);
            if (taxiCars == null)
            {
                return HttpNotFound();
            }
            return View(taxiCars);
        }

        // POST: ManageTaxiCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxiCars taxiCars = db.TaxiCars.Find(id);
            db.TaxiCars.Remove(taxiCars);
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
