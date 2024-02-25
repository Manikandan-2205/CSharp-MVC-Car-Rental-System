using SwiftRent.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SwiftRent.Controllers
{
    public class ReturnCarController : Controller
    {
        SwiftRentEntities db = new SwiftRentEntities();

        public ActionResult ReturnCar_index()
        {
            var ReturnCar = (from r in db.ReturnCars
                             join c in db.Carregs on r.CarNo equals c.CarNo
                             select new ReturnCarViewModel
                             {
                                 Id = r.Id,
                                 CarNo = r.CarNo,
                                 ReturnDate = r.ReturnDate,
                                 Elsp = r.Elsp /*?? 0*/,
                                 Fine = r.Fine /*?? 0*/,
                                 Available = c.Available
                             }).ToList();

            return View(ReturnCar);
        }

        [HttpGet]
        public ActionResult GetCar()
        {
            var car = db.Carregs.ToList();
            return Json(car, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetData(string CarNo)
        {
            var CarNum = (from s in db.Rentals
                          where s.CarId == CarNo
                          select new
                          {
                              StartDate = s.StartDate,
                              EndDate = s.EndDate,
                              CustId = s.CustId,
                              CarNo = s.CarId,
                              Fee = s.Fee,
                              ExpiredDays = SqlFunctions.DateDiff("day", s.EndDate, s.StartDate)
                          }).ToArray();
            return Json(CarNum, JsonRequestBehavior.AllowGet);
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
        public ActionResult Save(ReturnCarViewModel Returned)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Save returned car information
                        var returnedCar = new ReturnCar
                        {
                            CarNo = Returned.CarNo,
                            ReturnDate = Returned.ReturnDate,
                            Elsp = Returned.Elsp,
                            Fine = Returned.Fine
                        };

                        db.ReturnCars.Add(returnedCar);

                        // Update Carregs
                        var car = db.Carregs.SingleOrDefault(e => e.CarNo == Returned.CarNo);
                        if (car == null)
                        {
                            TempData["ErrorMessage"] = "Car not found";
                            return RedirectToAction("Index");
                        }

                        car.Available = "Yes";
                        db.Entry(car).State = EntityState.Modified;

                        db.SaveChanges();
                        transaction.Commit();

                        // Return a success message with a 200 status code
                        return Json(new { success = true, message = "Data saved successfully" });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        TempData["ErrorMessage"] = "Error saving data: " + ex.Message;
                    }
                }
            }

            // If ModelState is not valid, return to the view with validation errors
            return Json(new { success = false, message = "Error saving data. Please try again." });
        }


    }
}
