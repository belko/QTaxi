using QTaxi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
namespace QTaxi.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private TaxiEntities db = new TaxiEntities();
        private string userKey;
        List<Taxi> taxiIds;

        // GET: Orders
        public ActionResult Index()
        {

            userKey = User.Identity.GetUserId();

            taxiIds = (from m in db.TaxiManagers
                       join t in db.Taxi on m.taxi_id equals t.Id
                       where m.user_id == userKey
                       select t).ToList();
            if (taxiIds.Count() > 0)
                ViewBag.TaxiName = taxiIds[0].title;
            return View();
        }

        
    }
}