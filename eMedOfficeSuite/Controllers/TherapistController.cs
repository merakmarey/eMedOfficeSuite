using DataEntities;
using DataLog;
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
                var _apiClient = new ApiClient<List<Therapist>>();

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var _therapists = _apiClient.Get(_apiClient.TherapistTypestUrl, token);

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

                var therapistTypes = _apiClient.Get(_apiClient.TherapistTypestUrl, token);

                model.taxTypes = taxTypes;
                model.therapistTypes = therapistTypes;
                model.genderTypes = genderTypes;

                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View();
        }

    }
}