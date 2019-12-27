﻿using InsuranceQuote.Models;
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

                var quoteVms = new List<QuoteVm>();
                foreach (var quote in getquotes)
                {
                    var quoteVm = new QuoteVm
                    {
                        FirstName = quote.FirstName,
                        LastName = quote.LastName,
                        Email = quote.Email,
                        FinalQuote = Convert.ToInt32(quote.FinalQuote)
                    };
                    quoteVms.Add(quoteVm);
                }
            }
            return View();
        }
    }
}