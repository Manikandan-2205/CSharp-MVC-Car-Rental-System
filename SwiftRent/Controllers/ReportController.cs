using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SwiftRent.Models; // Import necessary namespace for your entities
using SwiftRent.SwiftRentDataSetTableAdapters; // Import necessary namespace for your view models

namespace SwiftRental.Controllers
{
    public class ReportController : Controller
    {
        private readonly SwiftRentEntities db;

        public ReportController()
        {
            db = new SwiftRentEntities(); // Initialize your DbContext
        }

        // Action method to handle GET request to display the report view
        public ActionResult Report()
        {
            try
            {
                // Assuming you have a method to fetch all rentals
                List<Rental> rentals = db.Rentals.ToList(); // Implement this method accordingly

                // Transform rentals to RentalReportViewModel
                List<RentalReportViewModel> reportViewModels = rentals.Select(rental => new RentalReportViewModel
                {
                    CustomerName = rental.Customer != null ? rental.Customer.CustName : null,
                    StartDate = rental.StartDate ?? DateTime.MinValue,
                    EndDate = rental.EndDate ?? DateTime.MinValue,
                    ReturnDate = rental.ReturnCar != null ? rental.ReturnCar.ReturnDate ?? DateTime.MinValue : DateTime.MinValue,
                    ExpiredDays = rental.ReturnCar != null ? rental.ReturnCar.Elsp ?? 0 : 0,
                    Fee = rental.Fee ?? 0,
                    Fine = rental.ReturnCar != null ? rental.ReturnCar.Fine ?? 0 : 0
                }).ToList();


                return View(reportViewModels);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                ViewBag.ErrorMessage = "An error occurred while generating the report.";
                return View(new List<RentalReportViewModel>()); // Return an empty list to the view
            }
        }

        // Action method to handle GET request to fetch rentals based on CarNo
        public ActionResult RentalsByCarNo(string carNo)
{
    try
    {
        // Assuming you have a method to fetch rentals based on CarNo
        List<Rental> rentals = db.Rentals.Where(r => r.CarId == carNo).ToList(); // Implement this method accordingly

        // Transform rentals to RentalReportViewModel
        List<RentalReportViewModel> reportViewModels = rentals.Select(rental => new RentalReportViewModel
        {
            CustomerName = rental.Customer.CustName,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            ReturnDate = rental.ReturnCar.ReturnDate,
            ExpiredDays = rental.ReturnCar.Elsp,
            Fee = rental.Fee,
            Fine = rental.ReturnCar.Fine
        }).ToList();

        return View("Report", reportViewModels);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it as appropriate
        ViewBag.ErrorMessage = "An error occurred while fetching rentals by CarNo.";
        return View("Report", new List<RentalReportViewModel>()); // Return an empty list to the view
    }
}

    }
}
