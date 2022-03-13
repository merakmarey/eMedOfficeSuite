using System;
using System.Linq;

namespace DataEntities.TherapistEntity
{
    public class Therapist 
    {
        public int therapistId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string companyName { get; set; }
        public string therapistLevel { get; set; }
        public Nullable<int> therapistTaxType { get; set; }
        public Nullable<int> therapistType { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string education { get; set; }
        public Nullable<int> gender { get; set; }
        public string ssn { get; set; }
        public string languages { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public Nullable<int> city { get; set; }
        public Nullable<int> state { get; set; }
        public string zip { get; set; }
        public Nullable<int> country { get; set; }
        public Nullable<System.DateTime> hiredate { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> supervisorId { get; set; }
        public Nullable<System.DateTime> terminationdate { get; set; }
        public string password { get; set; }
        public string fein { get; set; }
        public string npi { get; set; }
        public string license { get; set; }
        public string provider { get; set; }

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
                
            }
        }
    }
}
