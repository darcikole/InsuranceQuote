using InsuranceQuote.Models;
using InsuranceQuote.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceQuote.Controllers
{ 
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {
                var getquotes = db.Quotes.ToList();

                var quoteVms = new List<ViewModel.Quote>();
                foreach (var quote in getquotes)
                {
                    var quoteVm = new ViewModel.Quote
                    {
                        Id = quote.Id,
                        FirstName = quote.FirstName,
                        LastName = quote.LastName,
                        Email = quote.Email,
                        FinalQuote = Convert.ToDecimal(quote.FinalQuote)
                    };
                    quoteVms.Add(quoteVm);
                }
                return View(quoteVms);
            }
            
        }
    }
}