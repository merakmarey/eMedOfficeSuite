using ExtensionMethods;
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
using DataEntities.TherapistEntity;
using System.Configuration;
using DataEntities.Therapist;

namespace eMedOfficeSuite.Controllers
{

    [Authorize(Roles = (DataEntities.Users.UserRolesNames.Super))]
    public class TherapistController : Controller
    {
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;

        public Action UnauthorizedAction = (() => { UserController t = new UserController(); t.Logout(); });
    
        // GET: Therapist
        public ActionResult Index()
        {

            try
            {
                var _apiClient = new ApiClient<List<TherapistListItem>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var _therapists = _apiClient.Get(_apiClient.TherapistListUrl, token);

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

                var _apiClient = new ApiClient<Dictionary<int, string>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var taxTypes = _apiClient.Get(_apiClient.TaxTypestUrl, token);

                var genderTypes = _apiClient.Get(_apiClient.GenderTypesUrl, token);

                var stateTypes = _apiClient.Get(_apiClient.StatestUrl, token);

                var therapistTypes = _apiClient.Get(_apiClient.TherapistTypesUrl, token);

                var therapistStatusTypes = _apiClient.Get(_apiClient.TherapistStatusTypestUrl, token);

                var therapistSupervisors = _apiClient.Get(_apiClient.TherapistGetSupervisorsUrl, token);

                model.taxTypes = taxTypes;
                model.therapistTypes = therapistTypes;
                model.genderTypes = genderTypes;
                model.stateTypes = stateTypes;
                model.therapistStatusTypes = therapistStatusTypes;
                model.supervisors = therapistSupervisors;
                model.cities = null;


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

                    var _therapist = new therapist();

                    _therapist.FromForm(model);

                    string validChars = "ABCDEFGHIJKmnlopqrstuvwxyz1234567890!@#$%^&*";

                    Random random = new Random();

                    char[] chars = new char[10];

                    for (int i = 0; i < 10; i++)
                    {
                        chars[i] = validChars[random.Next(0, validChars.Length - 1)];
                    }

                    _therapist.password = new string(chars);

                    var _apiClient = new ApiClient<Boolean>(UnauthorizedAction);

                    _apiClient.addBody(_therapist);

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                    var result = _apiClient.Post(_apiClient.TherapistAddUrl, token);
                }
            }
            catch (Exception ex) {
                Log.AddEntry(ex);
            }

            return Redirect("/therapist");
        }
        public ActionResult Edit(int id)
        {
            try
            {
                dynamic model = new ExpandoObject();

                var _apiClient = new ApiClient<Dictionary<int, string>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var taxTypes = _apiClient.Get(_apiClient.TaxTypestUrl, token);
                var genderTypes = _apiClient.Get(_apiClient.GenderTypesUrl, token);
                var stateTypes = _apiClient.Get(_apiClient.StatestUrl, token);
                var therapistTypes = _apiClient.Get(_apiClient.TherapistTypesUrl, token);
                var therapistStatusTypes = _apiClient.Get(_apiClient.TherapistStatusTypestUrl, token);
                var therapistSupervisors = _apiClient.Get(_apiClient.TherapistGetSupervisorsUrl, token);
               

                var __apiClient = new ApiClient<Therapist>(UnauthorizedAction);

                var therapist = __apiClient.Get(__apiClient.TherapistGetUrl + "/" + id.ToString(), token);

                var cities = _apiClient.Get(_apiClient.CitiesUrl + "/" + therapist.city.ToString(), "");

                model.taxTypes = taxTypes;
                model.therapistTypes = therapistTypes;
                model.genderTypes = genderTypes;
                model.stateTypes = stateTypes;
                model.therapistStatusTypes = therapistStatusTypes;
                model.supervisors = therapistSupervisors;
                model.cities = cities;

                model.therapist = therapist;

                String _bds = model.therapist.birthdate.ToString();
                String _hds = model.therapist.hiredate.ToString();
                String _tds = model.therapist.terminationdate.ToString();

                model._birthdate = _bds.MutedOnlyDate();
                model._hiredate = _hds.MutedOnlyDate();
                model._terminationdate = _tds.MutedOnlyDate();


                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var _apiClient = new ApiClient<Boolean>(UnauthorizedAction);

                    var _therapist = new therapist();
                    
                    _therapist.FromForm(model);

                    _therapist.therapistId = id;

                    _apiClient.addBody(_therapist);

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                    var result = _apiClient.Post(_apiClient.TherapistUpdateUrl, token);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return Redirect("/therapist");
        }
    }
}