using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwiftRent.Models;
using System.Data.Entity;

namespace SwiftRent.Controllers
{
    public class RentController : Controller
    {
        SwiftRentEntities db = new SwiftRentEntities();
        //
        // GET: Rent
        public ActionResult Index()
        {
            var Result = (from r in db.Rentals
                          join c in db.Carregs on r.CarId equals c.CarNo
                          select new RentalViewModel
                          {
                              Id = r.Id,
                              CarId = r.CarId,
                              CustId = r.CustId,
                              Fee = r.Fee,
                              StartDate = r.StartDate,
                              EndDate = r.EndDate,
                              Available = c.Available
                          }).ToList();

            return View(Result);
        }

        [HttpGet]
        public ActionResult GetCar()
        {
            try
            {
                var car = db.Carregs.ToList();
                return Json(car, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetId(int id)
        {
            try
            {
                var CustomerName = (from s in db.Customers where s.Id == id select s.CustName).ToList();
                return Json(CustomerName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetName(string Name)
        {
            try
            {
                var CustomerId = (from s in db.Customers where s.CustName == Name select s.Id).ToList();
                return Json(CustomerId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetAvil(string CarNo)
        {
            try
            {
                var CarAvil = (from s in db.Carregs where s.CarNo == CarNo select s.Available).FirstOrDefault();
                return Json(CarAvil, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Save(Rental rent)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rent);

                var Car = db.Carregs.SingleOrDefault(name => name.CarNo == rent.CarId);
                if (Car == null)
                    return HttpNotFound("CarNo Is Not Found");

                Car.Available = "No";
                db.Entry(Car).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }

    }
}