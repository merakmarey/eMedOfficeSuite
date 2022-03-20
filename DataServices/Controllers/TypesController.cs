using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataServices.Controllers
{
    public class TypesController : ApiController
    {
        public Dictionary<int, string> GetTaxTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.tax_types.Select(t => new { t.taxtypeid, t.taxtype }).ToDictionary(t=>t.taxtypeid, t=>t.taxtype.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetGenderTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.genders.Select(t => new { t.genderId, t.gender1}).ToDictionary(t => t.genderId, t => t.gender1.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetTherapistTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.therapist_types.Select(t => new { t.therapisttypeId, t.therapisttype }).ToDictionary(t => t.therapisttypeId, t => t.therapisttype.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetStatesTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.states.Select(t => new { t.geostateid, t.statenamelong }).ToDictionary(t => t.geostateid, t => t.statenamelong.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetCityTypes(int id)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.cities.Where(k=>k.stateId==id).Select(t => new { t.cityId, t.cityName, t.stateId }).ToDictionary(t => t.cityId, t => t.cityName.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetTherapistStatusTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.therapist_status.Select(t => new { t.therapist_statusId, t.therapiststatus }).ToDictionary(t => t.therapist_statusId, t => t.therapiststatus.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetDirectors()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.directors.Select(t => new { t.directorId, t.directorFirstName, t.directorMiddleName, t.directorLastName }).ToDictionary(t => t.directorId, t => String.Join(" ",new[] { t.directorFirstName, t.directorMiddleName, t.directorLastName }).Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetPatientRelationshipsTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.relationshipTypes.Select(t => new { t.relationshipId, t.relationship}).ToDictionary(t => t.relationshipId, t => t.relationship.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetServiceLocationTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.service_locationTypes.Select(t => new { t.serviceLocationId, t.serviceLocation }).ToDictionary(t => t.serviceLocationId, t => t.serviceLocation.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
        public Dictionary<int, string> GetPayerTypes()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var query = db.payerTypes.Select(t => new { t.payerId, t.payerName }).ToDictionary(t => t.payerId, t => t.payerName.Trim());
                    return query;
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
            return null;
        }
    }
}
