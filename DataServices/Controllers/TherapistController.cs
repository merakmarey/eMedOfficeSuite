using DataEntities;
using DataLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DataServices.Controllers
{
    [Authorize(Roles=(DataEntities.Users.UserRolesNames.Super))]
    public class TherapistController : ApiController
    {
        public DataEntities.TherapistEntity.Therapist GetTherapist(int id)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var query = (from t in db.therapists
                                where t.therapistId == id
                                select t).AsEnumerable()
                                .Select(o=> o.ToTherapist());

                    var therapist = query.FirstOrDefault();

                    return therapist;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }

        public Dictionary<int, string> GetSuperVisors()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var query = db.therapists.Select(t => new { t.therapistId, t.firstName, t.lastName, t.middleName })
                        .ToDictionary(t => t.therapistId, t => t.firstName.Trim() + t.lastName.Trim());

                    return query;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }
        public IEnumerable<DataEntities.TherapistEntity.Therapist> GetTherapists()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {


                    var query = (from t in db.therapists
                                 select t).AsEnumerable()
                                .Select(o => o.ToTherapist());


                    var therapists = query.ToList<DataEntities.TherapistEntity.Therapist>();
                    if (therapists != null)
                        return therapists;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }
        public bool AddTherapist(therapist therapist)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    /* pwd encryption */

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(therapist.password);
                    data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                    therapist.password = System.Text.Encoding.ASCII.GetString(data);

                    var t = db.therapists;

                    t.Add(therapist);

                    var rows = db.SaveChanges();

                    return (rows>0?true:false);
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            
            return false;
        }

        public bool UpdateTherapist(therapist therapist)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var t = db.therapists.Find(therapist.therapistId);

                    if (t!=null)
                    {
                        therapist.password = t.password;
                        db.Entry(t).CurrentValues.SetValues(therapist);
                    } else
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
    }
}
