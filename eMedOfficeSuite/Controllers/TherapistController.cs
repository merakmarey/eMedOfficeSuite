using DataEntities;
using DataLog;
using DataServices;
using eMedOfficeSuite.ApiClient;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eMedOfficeSuite.Controllers
{
   
    [Authorize(Roles = (DataEntities.Users.UserRolesNames.Super))]
    public class TherapistController : Controller
    {
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;
        // GET: Therapist
        public ActionResult Index()
        {

            try
            {
                var _apiClient = new ApiClient<List<therapist>>();

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var _therapists = _apiClient.Get(_apiClient.TherapistTypesUrl, token);

                return View(_therapists);
                
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return View();
        }
        public ActionResult Add()
        {
            try
            {
                dynamic model = new ExpandoObject();

                var _apiClient = new ApiClient<Dictionary<int, string>>();

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var taxTypes = _apiClient.Get(_apiClient.TaxTypestUrl, token);

                var genderTypes = _apiClient.Get(_apiClient.GenderTypestUrl, token);

                var stateTypes = _apiClient.Get(_apiClient.StatestUrl, token);

                var therapistTypes = _apiClient.Get(_apiClient.TherapistTypesUrl, token);

                var therapistStatusTypes = _apiClient.Get(_apiClient.TherapistStatusTypestUrl, token);

                model.taxTypes = taxTypes;
                model.therapistTypes = therapistTypes;
                model.genderTypes = genderTypes;
                model.stateTypes = stateTypes;
                model.therapistStatusTypes = therapistStatusTypes;
                model.supervisor = null;


                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _threapist = new therapist()
                    {
                        therapistTaxType =  Int32.Parse(model["taxtype"]),
                        address1 = model["address1"],
                        address2 = model["address2"],
                        city = model["city"],
                        state = model["state"],
                        zip = model["zip"],
                        country = "US",

                        companyName = model["companyName"],
                        education = model["education"],
                        email = model["email"],
                        fein = model["fein"],
                        firstName = model["firstName"],
                        lastName = model["lastName"],
                        gender = Int32.Parse(model["gender"]),
                        birthdate = DateTime.Parse(model["birthdate"]),
                        hiredate = DateTime.Parse(model["hiredate"]),
                        therapistLevel = model["therapistlevel"],
                        languages = model["languages"],
                        license = model["license"],
                        middleName = model["middleName"],
                        phone = model["phone"],
                        provider = model["provider"],
                        ssn = model["ssn"],
                        status = true,
                        supervisorId = Int32.Parse(model["supervisor"]),
                        terminationdate =  DateTime.Parse(model["terminationdate"]),
                        therapistType = Int32.Parse(model["therapistType"])
                    };

                    var _apiClient = new ApiClient<List<therapist>>();

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                    var _therapists = _apiClient.Post(_apiClient.TherapistAddUrl, token);
                }
            }
            catch (Exception ex) {
                Log.AddEntry(ex);
            }


            return View();
        }

    }
}