using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QTaxi.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace QTaxi.Controllers.Api
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private TaxiEntities db = new TaxiEntities();
        private string userKey;
        List<int> taxiIds;
        private OrdersController() 
        {
            userKey = User.Identity.GetUserId();

            taxiIds = (from m in db.TaxiManagers
                                 join t in db.Taxi on m.taxi_id equals t.Id
                                 where m.user_id == userKey
                                 select t.Id).ToList();
        }
        // GET: api/Orders
        public OrderModel[] GetOrder()
        {
            

            if (taxiIds.Count() == 0)
                return new OrderModel[0];

            return db.Order
                .Where(o => o.taxi_id == null || taxiIds.Contains(o.taxi_id.Value))
                .Select(o => new OrderModel
            {
                id = o.id,
                address_from = o.address_from,
                address_to = o.address_to,
                car_id = o.car_id,
                car_name = o.Car.car_mark + " " + o.Car.car_model,
                taxi_id = o.taxi_id,
                taxi_name = o.Taxi.title,
                status_id = o.status_id,
                status = o.OrderStatus.name

            }).ToArray();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.create_date = DateTime.UtcNow;
            db.Order.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.id == id) > 0;
        }
    }
}