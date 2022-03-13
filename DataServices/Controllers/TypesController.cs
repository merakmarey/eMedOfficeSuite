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
    }
}
