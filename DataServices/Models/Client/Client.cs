using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataEntities.Client
{
    public class Client : client
    {
        public Client() { }
        public Client(dynamic t)
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
