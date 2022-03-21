using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataEntities.UserEntity;
using DataEntities.Users;
using DataLog;
using eMedOfficeSuite.ApiClient;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using RestSharp;

namespace eMedOfficeSuite.ApiClient
{
    public class ApiClient<T>
    {
        private object _body;

        public Action UnauthorizedEvent;

        private System.Net.HttpStatusCode _lastStatus;
        public System.Net.HttpStatusCode lastStatus { get { return _lastStatus; } }

        public readonly string TokenUrl =                   "/token";
        public readonly string UserUrl =                    "/api/user";

        /*MISC*/
        public readonly string TaxTypestUrl =               "/api/types/gettaxtypes";
        public readonly string GenderTypesUrl =            "/api/types/getgendertypes";
        public readonly string TherapistTypesUrl =          "/api/types/gettherapisttypes";
        public readonly string TherapistStatusTypestUrl =   "/api/types/gettherapiststatustypes";
        public readonly string StatestUrl =                 "/api/types/getstatestypes";
        public readonly string CitiesUrl =                  "/api/types/getcitytypes";
        public readonly string DirectorsUrl =               "/api/types/getdirectors";
        public readonly string PatientRelationshipsTypesUrl = "/api/types/getpatientrelationshipstypes";
        public readonly string ServiceLocationsTypesUrl =   "/api/types/getservicelocationtypes";
        public readonly string PayerTypesUrl =              "/api/types/getpayertypes";
        


        /* THERAPISTS */
        public readonly string TherapistAddUrl =            "/api/therapist/addtherapist";
        public readonly string TherapistListUrl =           "/api/therapist/gettherapistlist";
        public readonly string TherapistGetUrl =            "/api/therapist/gettherapist";
        public readonly string TherapistGetSupervisorsUrl = "/api/therapist/getsupervisors";
        public readonly string TherapistUpdateUrl         = "/api/therapist/updateTherapist";


        /* CLIENTS */

        public readonly string ClientAddUrl = "/api/client/addclient";
        public readonly string ClientListUrl = "/api/client/getclientlist";


        public RestClient client;
        public ApiClient(Action UnauthorizedAction)
        {
            UnauthorizedEvent = UnauthorizedAction;

            var url = ConfigurationManager.AppSettings.Get("DataServicesBaseUrl");

            var options = new RestClientOptions(url)
            {
                Timeout = 10000
                //ThrowOnAnyError = true
            };

            client = new RestClient(options);
        }

        public void addBody(object body)
        {
            _body = body;
        }

        public Dictionary<string, object> Auth(string username, string password)
        {
            try
            {
                var request = new RestRequest(TokenUrl);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", username);
                request.AddParameter("password", password);
                request.AddParameter("grant_type", "password");

                var loginTask = Task.Run(async () => await client.PostAsync(request, cancellationToken: System.Threading.CancellationToken.None));
                var loginResponse = loginTask.GetAwaiter().GetResult();
                var loginResponseElements = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResponse.Content);

                return loginResponseElements;
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }


        public T Post(string url, string token)
        {

            var userRequest = new RestRequest(url);

            if (_body != null)
            {
                userRequest.AddBody(_body);
            }


            try
            {

                userRequest.AddHeader("Authorization", "Bearer " + token);

                var userTask = Task.Run(async () => await client.ExecutePostAsync(userRequest, cancellationToken: System.Threading.CancellationToken.None));
                var userResponse = userTask.GetAwaiter().GetResult();
                
                _lastStatus = userResponse.StatusCode;
               
                if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseToString = JsonConvert.DeserializeObject(userResponse.Content).ToString();
                    switch (responseToString.ToLower())
                    {
                        case "true":
                            return (T)(object)true;
                        case "false":
                            return (T)(object)false;
                    }                     
                    var response = JsonConvert.DeserializeObject<T>(responseToString);

                    return response;
                }
                if ((UnauthorizedEvent != null) && (_lastStatus == System.Net.HttpStatusCode.Unauthorized))
                {
                    UnauthorizedEvent();
                }


            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return default(T);
        }
        public T Get(string url, string token)
        {
            try
            {
                var userRequest = new RestRequest(url);

                if (_body != null)
                {
                    userRequest.AddBody(_body);
                }

                userRequest.AddHeader("Authorization", "Bearer " + token);

                var userTask = Task.Run(async () => await client.ExecuteGetAsync(userRequest, cancellationToken: System.Threading.CancellationToken.None));
                var userResponse = userTask.GetAwaiter().GetResult();

                _lastStatus = userResponse.StatusCode;

                if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var userResponseToString = JsonConvert.DeserializeObject(userResponse.Content).ToString();
                    var user = JsonConvert.DeserializeObject<T>(userResponseToString);
                    return user;
                }
                if ((UnauthorizedEvent != null)  && (_lastStatus == System.Net.HttpStatusCode.Unauthorized))
                {
                    UnauthorizedEvent();
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return default(T);
        }

    }
}