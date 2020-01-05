using InsuranceQuote.Models;
using InsuranceQuote.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InsuranceQuote.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewQuote(string firstName, string lastName, string email, DateTime dob,
                                    int carYear, string carMake, string carModel,
                                    bool dui, int? tickets, bool fullCoverage, decimal? finalQuote)
        {


            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {

                var newquote = new Models.Quote
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    DOB = dob,
                    CarYear = Convert.ToInt16(carYear),
                    CarMake = carMake,
                    CarModel = carModel,
                    DUI = dui,
                    Tickets = Convert.ToInt16(tickets),
                    FullCoverage = fullCoverage,
                    FinalQuote = Convert.ToDecimal(finalQuote)
                };

                
                // Taking newquote input and calculating result
                int basePrice = 50;
                
                int age = DateTime.Now.Year - dob.Year;

                int ageQuote;
                if (age < 18)
                {
                    ageQuote = 100;
                }
                else if (age > 18 && age < 25 || age > 100)
                {
                    ageQuote = 25;
                }
                else
                {
                    ageQuote = 0;
                }


                int carAgeQuote;
                if (carYear < 2000 || carYear > 2015)
                {
                    carAgeQuote = 25;
                }
                else
                {
                    carAgeQuote = 0;
                }


                int porcheQuote;
                
                if (carMake == "Porche")
                {
                    porcheQuote = 25;
                }
                else
                {
                    porcheQuote = 0;
                }


                int porcheX;
                if (carModel == "911 Carerra")
                {
                    porcheX= 25;
                }
                else
                {
                    porcheX = 0;
                }


                int ticketsQuote;
                if (tickets > 0)
                {
                    ticketsQuote = Convert.ToInt32(tickets) * 10;
                }
                else
                {
                    ticketsQuote = 0;
                }


                int runningTotal = basePrice + ageQuote + carAgeQuote + porcheQuote + porcheX + ticketsQuote;

                float duiQuote = dui == true ? Convert.ToInt32(runningTotal * 0.25) : 0;

                float updateTotal = runningTotal + duiQuote;

                float coverageQuote = fullCoverage == true ? Convert.ToInt32(updateTotal * 0.50) : 0;

                decimal newTally = Convert.ToDecimal(updateTotal) + Convert.ToDecimal(coverageQuote);

                finalQuote = newTally;

                db.Quotes.Add(newquote);
                db.SaveChanges();
            } 

           
            return View(finalQuote);

        }
    }
}
