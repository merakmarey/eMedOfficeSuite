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

        public Client GetClient(int id)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var query = (from t in db.clients
                                 where t.clientId == id
                                 select t).AsEnumerable()
                                .Select(o =>o.ToClient());

                    var client = query.FirstOrDefault();

                    return client;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
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

        public bool UpdateClient(Client Client)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var t = db.clients.Find(Client.clientId);

                    if (t != null)
                    {
                        db.Entry(t).CurrentValues.SetValues(Client);
                    }
                    else
                    {
                        return false;
                    }

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

        public List<client_assigments> GetClientAssigments(int id)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.client_assigments.Where(c => c.endDate==null).Select(t=> new client_assigments() { 
                         assignmentId = t.assignmentId,
                         clientId = t.clientId,
                         therapistId = t.therapistId,
                         startDate = t.startDate,
                         supervisorId = t.supervisorId,
                         therapistTypeId = t.therapistTypeId
                        });
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