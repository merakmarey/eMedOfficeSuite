using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities
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
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public Nullable<System.DateTime> hiredate { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> supervisorId { get; set; }
        public Nullable<System.DateTime> terminationdate { get; set; }
    }
}
