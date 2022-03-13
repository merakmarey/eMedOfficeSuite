using eMedOfficeSuite.ApiClient;
using System.Collections.Generic;
using System.Web.Http;

namespace eMedOfficeSuite.Controllers
{
    public class PassController : ApiController
    {
        public Dictionary<int, string> cities(int id)
        {
            var _apiClient = new ApiClient<Dictionary<int, string>>();

            var cities = _apiClient.Get(_apiClient.CitiesUrl+"/"+id.ToString(), "");

            return cities;
        }
    }
}
