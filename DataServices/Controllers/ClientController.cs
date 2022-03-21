using DataEntities.Client;
using DataLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using static DataEntities.Users.UserRolesNames;

namespace DataServices.Controllers
{
    [Authorize(Roles = (DataEntities.Users.UserRolesNames.Super))]
    public class ClientController : ApiController
    {
        // GET: Client
        
        public bool AddClient(client client)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var t = db.clients;

                    t.Add(client);

                    var rows = db.SaveChanges();

                    return (rows > 0 ? true : false);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }

            return false;
        }

        public List<ClientListItem> GetClientList()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.clients.Select(c => new ClientListItem() { clientId = c.clientId, clientFirstname = c.firstName, clientMiddleName = c.middleName, clientLastName = c.lastName});
                    return query.ToList();
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