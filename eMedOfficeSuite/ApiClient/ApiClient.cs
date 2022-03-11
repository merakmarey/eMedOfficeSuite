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
        private System.Net.HttpStatusCode _lastStatus;
        public System.Net.HttpStatusCode lastStatus { get { return _lastStatus; } }

        public readonly string TokenUrl = "/token";
        public readonly string UserUrl = "/api/user";

        public readonly string TaxTypestUrl = "/api/types/gettaxtypes";
        public readonly string GenderTypestUrl = "/api/types/getgendertypes";
        public readonly string TherapistTypesUrl = "/api/types/gettherapisttypes";
        public readonly string TherapistStatusTypestUrl = "/api/types/gettherapiststatustypes";
        public readonly string StatestUrl = "/api/types/getstatestypes";

        public readonly string TherapistAddUrl = "/api/therapist/addtherapist";
        public RestClient client;
        public ApiClient()
        {
            var url = ConfigurationManager.AppSettings.Get("DataServicesBaseUrl");

            var options = new RestClientOptions(url)
            {
                Timeout = 1000
                //ThrowOnAnyError = true
            };

            client = new RestClient(options);
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

            try
            {

                userRequest.AddHeader("Authorization", "Bearer " + token);

                var userTask = Task.Run(async () => await client.ExecutePostAsync(userRequest, cancellationToken: System.Threading.CancellationToken.None));
                var userResponse = userTask.GetAwaiter().GetResult();
                
                _lastStatus = userResponse.StatusCode;

                if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var userResponseToString = JsonConvert.DeserializeObject(userResponse.Content).ToString();
                    var user = JsonConvert.DeserializeObject<T>(userResponseToString);
                    return user;
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
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return default(T);
        }

    }
}