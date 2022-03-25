using DataEntities.Therapist;
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
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class RatesController : Controller
    {
        // GET: Rates
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;

        public Action UnauthorizedAction = (() => { UserController t = new UserController(); t.Logout(); });
        public ActionResult Index(int? id)
        {
            
            try
            {
                dynamic model = new ExpandoObject();

                var _apiClient = new ApiClient<List<TherapistListItem>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var _therapists = _apiClient.Get(_apiClient.TherapistListUrl, token);

                if (_therapists.Count==0)
                {
                    return View();
                }
                // documents

                var documentTypes_apiClient = new ApiClient<Dictionary<int, string>>(UnauthorizedAction);

                var _documentTypes = documentTypes_apiClient.Get(_apiClient.DocumentTypesUrl, token);

                // rates 

                var therapistId = (id == null ? _therapists.FirstOrDefault().therapistId : id);

                ViewBag.SelectedId = therapistId;

                var ratesTypes_apiClient = new ApiClient<List<rateType>>(UnauthorizedAction);

                var _rateTypes = ratesTypes_apiClient.Get(_apiClient.RateTypesUrl, token);

                // therapist rates

                var therapistRates_apiClient = new ApiClient<List<Rates>> (UnauthorizedAction);

                var _rates = therapistRates_apiClient.Get(_apiClient.GetActiveRatesUrl + "/" + therapistId, token);

                model.therapists = _therapists;

                model.rateTypes = _rateTypes;

                model.rates = new Dictionary<int,List<Rates>>();

                model.documentTypes = _documentTypes;

                foreach (var item in _rateTypes)
                {
                    model.rates.Add(item.rateTypeId,_rates.Where(g=>g.rateType==item.rateTypeId).ToList());
                }
                

                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return View();
           
        }
        [HttpPost]
        public ActionResult Index(int? id, FormCollection model)
        {
            return View();
        }
    }
}