using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataServices
{
    public class Rates : therapist_rates
    {
        public Rates() { }

        public Rates ToRates()
        {
            return new Rates(this);
        }
        public Rates(dynamic t)
        {
            string[] propertyNames = this.GetType().GetProperties().Select(p => p.Name).ToArray();
            var thisType = this.GetType();
            var me = this;
            try
            {
                foreach (var prop in propertyNames)
                {
                    var value = t.GetType().GetProperty(prop).GetValue(t);
                    thisType.GetProperty(prop).SetValue(me, value);
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddEntry(ex);
            }
        }
    }
}