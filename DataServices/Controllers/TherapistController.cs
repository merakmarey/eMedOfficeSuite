using DataEntities;
using DataLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataServices.Controllers
{
    [Authorize(Roles=(DataEntities.Users.UserRolesNames.Super))]
    public class TherapistController : ApiController
    {
        public IEnumerable<Therapist> GetTherapists()
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var query = from t in db.therapists
                                select new Therapist() {
                                    therapistId = t.therapistId,
                                    email = t.email,
                                    firstName = t.firstName,
                                    middleName = t.middleName,
                                    lastName = t.lastName,
                                    birthdate = t.birthdate,
                                    gender = t.gender,
                                    ssn = t.ssn,
                                    address1 = t.address1,
                                    address2 = t.address2,
                                    country = t.country,
                                    city = t.city,
                                    state = t.state,
                                    zip = t.zip,
                                    phone = t.phone,
                                    companyName = t.companyName,
                                    education = t.education,
                                    languages = t.languages,
                                    status = t.status,
                                    hiredate = t.hiredate,
                                    terminationdate = t.terminationdate,
                                    supervisorId = t.supervisorId,
                                    therapistLevel = t.therapistLevel,
                                    therapistTaxType = t.therapistTaxType,
                                    therapistType= t.therapistType
                                };

                    var therapists = query.ToList<Therapist>();
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
        public bool addTherapist(Therapist therapist)
        {
            return false;
        }
    }
}
