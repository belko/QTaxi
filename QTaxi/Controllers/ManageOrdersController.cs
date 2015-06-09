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
    public class ManageOrdersController : Controller
    {
        private TaxiEntities db = new TaxiEntities();

        // GET: ManageOrders
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.Car).Include(o => o.OrderStatus).Include(o => o.Taxi);
            return View(order.ToList());
        }

        // GET: ManageOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: ManageOrders/Create
        public ActionResult Create()
        {
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark");
            ViewBag.status_id = new SelectList(db.OrderStatus, "id", "name");
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title");
            return View();
        }

        // POST: ManageOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,address_from,address_to,taxi_id,car_id,client_id,client_phone,client_name,status_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", order.car_id);
            ViewBag.status_id = new SelectList(db.OrderStatus, "id", "name", order.status_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", order.taxi_id);
            return View(order);
        }

        // GET: ManageOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", order.car_id);
            ViewBag.status_id = new SelectList(db.OrderStatus, "id", "name", order.status_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", order.taxi_id);
            return View(order);
        }

        // POST: ManageOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,address_from,address_to,taxi_id,car_id,client_id,client_phone,client_name,status_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.car_id = new SelectList(db.Car, "Id", "car_mark", order.car_id);
            ViewBag.status_id = new SelectList(db.OrderStatus, "id", "name", order.status_id);
            ViewBag.taxi_id = new SelectList(db.Taxi, "Id", "title", order.taxi_id);
            return View(order);
        }

        // GET: ManageOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: ManageOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
