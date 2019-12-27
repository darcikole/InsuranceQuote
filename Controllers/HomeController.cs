using InsuranceQuote.Models;
using System;
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
                                    bool dui, int tickets, bool fullCoverage, int finalQuote = 50)
        {


            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {

                var newquote = new Quote
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
                };

                db.Quotes.Add(newquote);
                db.SaveChanges();

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
                    ticketsQuote = (tickets * (10 * 12));
                }
                else
                {
                    ticketsQuote = 0;
                }
                int runningTotal = basePrice + ageQuote + carAgeQuote + porcheQuote + porcheX + ticketsQuote;

                int duiQuote = dui == true ? Convert.ToInt32(runningTotal * 0.25) : 0;

                int updateTotal = runningTotal + duiQuote;

                int coverageQuote = fullCoverage == true ? Convert.ToInt32(updateTotal * 0.50) : 0;

                int newTally = updateTotal + coverageQuote;
                finalQuote = newTally;

                var getquote = new Quote
                { 
                    FinalQuote = finalQuote  
                };

                


                db.Quotes.Add(getquote);
                db.SaveChanges();
            }
                return View("Quote");
        }
    }
}
