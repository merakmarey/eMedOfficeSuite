using DataServices;
using System;
using System.Linq;

namespace DataEntities.TherapistEntity
{
    public class Therapist : therapist
    {
        public Therapist() { }
        public Therapist(dynamic t)
        {
            string[] propertyNames = this.GetType().GetProperties().Select(p => p.Name).ToArray();
            var thisType = this.GetType();
            var me = this;
            try
            {
                foreach (var prop in propertyNames)
                {
                    if (prop.ToLower() != "password")
                    {
                        var value = t.GetType().GetProperty(prop).GetValue(t);
                        thisType.GetProperty(prop).SetValue(me, value);
                    }
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
        }
    }
}
