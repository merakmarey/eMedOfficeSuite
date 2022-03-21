using DataEntities.Client;
using DataLog;
using DataServices;
using eMedOfficeSuite.ApiClient;
using Helpers;
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
    public class ClientController : Controller
    {
        // GET: Client
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;

        public Action UnauthorizedAction = (() => { UserController t = new UserController(); t.Logout(); });

        public ActionResult Index()
        {
            try
            {
                dynamic model = new ExpandoObject();

                var _apiClient = new ApiClient<List<ClientListItem>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var clientList = _apiClient.Get(_apiClient.ClientListUrl, token);

                return View(clientList);

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

                var directorTypes = _apiClient.Get(_apiClient.DirectorsUrl, token);

                var genderTypes = _apiClient.Get(_apiClient.GenderTypesUrl, token);

                var stateTypes = _apiClient.Get(_apiClient.StatestUrl, token);

                var relationshipTypes = _apiClient.Get(_apiClient.PatientRelationshipsTypesUrl, token);

                var serviceLocationTypes = _apiClient.Get(_apiClient.ServiceLocationsTypesUrl, token);

                var payerTypes = _apiClient.Get(_apiClient.PayerTypesUrl, token);

                model.relationshipToPatientTypes = relationshipTypes;
                model.taxTypes = taxTypes;
                model.directorTypes = directorTypes;
                model.serviceLocationTypes = serviceLocationTypes;
                model.genderTypes = genderTypes;
                model.stateTypes = stateTypes;
                model.payerTypes = payerTypes;

                model.cityTypes = new Dictionary<int, string> (){ { 0, "Select State" } };


                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View("/client");
        }


        [HttpPost]
        public ActionResult Add(FormCollection model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var _client = new DataEntities.Client.Client();

                    var success = TryUpdateModel<client>(_client,model);

                    var _apiClient = new ApiClient<Boolean>(UnauthorizedAction);

                    _apiClient.addBody(_client);

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                    var result = _apiClient.Post(_apiClient.ClientAddUrl, token);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return Redirect("/client");
        }
        public ActionResult Edit()
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