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
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ClientController : Controller
    {
        // GET: Client
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;

        public Action UnauthorizedAction = (() => { UserController t = new UserController(); t.Logout(); });

        public ActionResult Index()
        {
            try
            {

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
        public ActionResult Edit(int id)
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

                model.cityTypes = new Dictionary<int, string>() { { 0, "Select State" } };

                var __apiClient = new ApiClient<Client>(UnauthorizedAction);

                var client = __apiClient.Get(__apiClient.ClientGetUrl + "/" + id.ToString(), token);

                model.client = client;

                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View("/client");
        }
        public ActionResult Assigment(int? id)
        {
            try
            {
                dynamic model = new ExpandoObject();

                var _apiClient = new ApiClient<List<ClientListItem>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var clientList = _apiClient.Get(_apiClient.ClientListUrl, token);

                model.clients = clientList;

                ViewBag.SelectedId = (id==null?clientList.FirstOrDefault().clientId:id);

                return View(model);

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View();
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

        [HttpPost]
        public ActionResult Edit(int id,FormCollection model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var _client = new DataEntities.Client.Client();

                    var success = TryUpdateModel<client>(_client, model);

                    _client.primaryPayerId = model["primaryPayerId"];
                    _client.secondaryPayerId = model["secondaryPayerId"];

                    _client.clientId = id;

                    var _apiClient = new ApiClient<Boolean>(UnauthorizedAction);

                    _apiClient.addBody(_client);

                    var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                    var result = _apiClient.Post(_apiClient.ClientUpdateUrl, token);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return Redirect("/client");
        }
    }
}