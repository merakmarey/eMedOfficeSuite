using DataServices;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eMedOfficeSuite.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var fb = new FormBuilder();
            var c = new client();

            fb.exceptions.Add("clientId");

            fb.build(c);

            ViewBag.GeneratedForm = fb.html;


            return View();
        }
    }
}