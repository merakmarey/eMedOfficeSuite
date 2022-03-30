using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using DataEntities.Users;
using Newtonsoft.Json;
using DataLog;

namespace DataServices.Controllers
{
    public class UserController : ApiController
    {
        // GET: User
        [Authorize(Roles =(DataEntities.Users.UserRolesNames.Super))]
        public string Index()
        {
            try
            {
                var user = HttpContext.Current.GetOwinContext().Authentication.User;
                if (user.Identities.Count() > 0)
                {
                    var identity = user.Identities.First();
                    var usr = JsonConvert.DeserializeObject<UserIdentity>(identity.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.UserData).ToList().First().Value);

                    /* 
                    var usr = new UserIdentity()
                    {
                        roleId = Int32.Parse(identity.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.UserData).ToList().First().Value),
                        username = identity.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Name).ToList().First().Value,
                    };
                    */    
                    return JsonConvert.SerializeObject(usr);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }
    }
}