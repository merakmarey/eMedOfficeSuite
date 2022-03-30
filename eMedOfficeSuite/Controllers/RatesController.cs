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
            try
            {
                if (ModelState.IsValid)
                {
                    var _apiClient_TherapistList = new ApiClient<List<TherapistListItem>>(UnauthorizedAction);

                    var _apiClient_TherapistRates = new ApiClient<List<Rates>>(UnauthorizedAction);

                    var _apliClient_RatesTypeList = new ApiClient<List<rateType>>(UnauthorizedAction);

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;
                    var userId = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Thumbprint).ToList().First().Value;

                    var _therapists = _apiClient_TherapistList.Get(_apiClient_TherapistList.TherapistListUrl, token);
                    if (_therapists.Count == 0)
                    {
                        return View();
                    }

                    var therapistId = (id == null ? _therapists.FirstOrDefault().therapistId : id);

                    var _rates = _apiClient_TherapistRates.Get(_apiClient_TherapistRates.GetActiveRatesUrl + "/" + therapistId, token);

                    var _rateTypes = _apliClient_RatesTypeList.Get(_apliClient_RatesTypeList.RateTypesUrl, token);

                    foreach (var item in model.AllKeys)
                    {
                       

                        switch (item[0])
                        {
                            case 'i':
                                var decomposedAction = item.Split('-');
                                var thisRateType = _rateTypes.Where(rt => rt.rateTypeId.ToString() == decomposedAction[1]).FirstOrDefault();

                                if (!String.IsNullOrEmpty(model[item]))
                                {
                                    
                                    
                                    if (thisRateType!=null) 
                                    {
                                        var currentRate = _rates.Where(r => r.rateType == thisRateType.rateTypeId).FirstOrDefault();
                                       

                                        if (currentRate==null)
                                        {
                                            // there are no current rate, which is consistent with the i preposition
                                            var _apiClient_TherapistRateAction = new ApiClient<bool>(UnauthorizedAction);

                                            int? docId = null;

                                            if ((bool)thisRateType.allowMultiple)
                                            {
                                                // then there should be a s preposition
                                                if (model.AllKeys.Contains("s-" + decomposedAction[1]))
                                                {
                                                    docId = Int32.Parse(model["s-" + decomposedAction[1]]);
                                                }
                                            }

                                            _apiClient_TherapistRateAction.addBody(new therapist_rates() {
                                                rateType = thisRateType.rateTypeId,
                                                startDate = DateTime.Now,
                                                therapistId = (int)therapistId,
                                                rateValue = Decimal.Parse(model[item]),
                                                setByUserId = Int32.Parse(userId),
                                                documentId = docId
                                            });

                                            var result = _apiClient_TherapistRateAction.Post(_apiClient_TherapistRateAction.AddRateUrl, token);
                                        }
                                    }

                                }
                                break;
                            default:
                                int index=0;
                                int? _docId = null;

                                if (int.TryParse(item, out index))
                                {
                                    var _apiClient_TherapistRateAction = new ApiClient<bool>(UnauthorizedAction);

                                    _apiClient_TherapistRateAction.addBody(new therapist_rates()
                                    {
                                        rateId = index,
                                        startDate = DateTime.Now,
                                        rateValue = Decimal.Parse(model[item]),
                                        setByUserId = Int32.Parse(userId),
                                    });

                                    var result = _apiClient_TherapistRateAction.Post(_apiClient_TherapistRateAction.UpdateRateUrl, token);

                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View();
        }
    }
}