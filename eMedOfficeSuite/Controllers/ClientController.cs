using DataEntities.Client;
using DataEntities.TherapistEntity;
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
                var _apiClient_Therapists = new ApiClient<List<Therapist>>(UnauthorizedAction);
                var _apiClient_TherapistTypes = new ApiClient<Dictionary<int, string>>(UnauthorizedAction);
                var _apiClient_Assigments = new ApiClient<List<client_assigments>>(UnauthorizedAction);

                var token = Authentication.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Authentication).ToList().First().Value;

                var clientList = _apiClient.Get(_apiClient.ClientListUrl, token);

                var clientId = (id == null ? clientList.FirstOrDefault().clientId : id);

                ViewBag.SelectedId = clientId;

                model.clients = clientList;

                var therapistList = _apiClient_Therapists.Get(_apiClient.TherapistsGetUrl, token);

                var therapistTypes = _apiClient_TherapistTypes.Get(_apiClient.TherapistTypesUrl, token);

                var assigments = _apiClient_Assigments.Get(_apiClient_Assigments.ClientAssignments+"/"+ clientId, token);

                var rbtType = therapistTypes.Where(tt => tt.Value.ToLower().StartsWith("rbt")).FirstOrDefault().Key;
                var itdsType = therapistTypes.Where(tt => tt.Value.ToLower().StartsWith("itds")).FirstOrDefault().Key;
                var bcbaType = therapistTypes.Where(tt => tt.Value.ToLower().StartsWith("bcba")).FirstOrDefault().Key;
                var bcabaType = therapistTypes.Where(tt => tt.Value.ToLower().StartsWith("bcabc")).FirstOrDefault().Key;
                var analystType = therapistTypes.Where(tt => tt.Value.ToLower().StartsWith("analyst")).FirstOrDefault().Key;

                var rbtAssigment = assigments.Where(a => a.therapistTypeId == rbtType).Select(s => s).FirstOrDefault();
                model.rbt = therapistList.Where(t => t.therapistType == rbtType).Select(r => r).ToList();
                model.rbtSelected = (rbtAssigment == null ? null : rbtAssigment.therapistId);
                model.rbtSupSelected = (rbtAssigment == null ? null : rbtAssigment.supervisorId);

                var bcbaAssigment = assigments.Where(a => a.therapistTypeId == bcbaType).Select(s => s).FirstOrDefault();
                model.bcba = therapistList.Where(t => t.therapistType == bcbaType).Select(r => r).ToList();
                model.bcbaSelected = (bcbaAssigment == null ? null : bcbaAssigment.therapistId);
                model.bcbaSupSelected = (bcbaAssigment == null ? null : bcbaAssigment.supervisorId);

                var bcabaAssigment = assigments.Where(a => a.therapistTypeId == bcabaType).Select(s => s).FirstOrDefault();
                model.bcaba = therapistList.Where(t => t.therapistType == bcabaType).Select(r => r).ToList();
                model.bcabaSelected = (bcabaAssigment == null ? null : bcabaAssigment.therapistId);
                model.bcabaSupSelected = (bcabaAssigment == null ? null : bcabaAssigment.supervisorId);

                var analystAssigment = assigments.Where(a => a.therapistTypeId == analystType).Select(s => s).FirstOrDefault();
                model.analyst = therapistList.Where(t => t.therapistType == analystType).Select(r => r).ToList();
                model.analystSelected = (analystAssigment == null ? null : analystAssigment.therapistId);
                model.analystSupSelected = (analystAssigment == null ? null : analystAssigment.supervisorId);

                var itdsAssigment = assigments.Where(a => a.therapistTypeId == itdsType).Select(s => s).FirstOrDefault();
                model.itds = therapistList.Where(t => t.therapistType == itdsType).Select(r => r).ToList();
                model.itdsSelected = (itdsAssigment == null ? null : itdsAssigment.therapistId);
                model.itdsSupSelected = (itdsAssigment == null ? null : itdsAssigment.supervisorId);



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