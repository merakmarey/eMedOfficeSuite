using DataLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace DataServices.Controllers
{
    [Authorize(Roles = (DataEntities.Users.UserRolesNames.Super))]
    public class RatesController : ApiController
    {
        // GET: Client

        public bool AddRate(therapist_rates rate)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var t = db.therapist_rates;

                    t.Add(rate);

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

        public List<Rates> GetActiveRates(int id)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var query = (from t in db.therapist_rates
                                 where t.therapistId == id && t.endDate==null
                                 select t).AsEnumerable()
                                .Select(o => new Rates()
                                {
                                    therapistId = o.therapistId,
                                    endDate = o.endDate,
                                    startDate = o.startDate,
                                    rateId = o.rateId,
                                    rateType = o.rateType,
                                    rateValue = o.rateValue,
                                    setByUserId = o.setByUserId,
                                    documentId = o.documentId
                                }).ToList();

                    return query;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }

        public bool UpdateRate(therapist_rates rate)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {

                    var t = db.therapist_rates.Find(rate.rateId);

                    if (t != null)
                    {
                        t.endDate = DateTime.Now;
                        t.setByUserId = rate.setByUserId;
                        db.Entry(t).CurrentValues.SetValues(t);
                        var r = new therapist_rates()
                        {
                            startDate = DateTime.Now,
                            therapistId = t.therapistId,
                            documentId = t.documentId,
                            rateType = t.rateType,
                            rateValue = rate.rateValue,
                            setByUserId = rate.setByUserId,
                        };
                        
                        db.therapist_rates.Add(r);
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
    }
}