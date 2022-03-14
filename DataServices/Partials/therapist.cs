using DataEntities.TherapistEntity;
using ExtensionMethods;
using System;
using System.Web.Mvc;

namespace DataServices
{
    public partial class therapist
    {
        public Therapist ToTherapist()
        {
            return new Therapist(this);
        }

        public void FromForm(FormCollection model)
        {
         
            therapistTaxType = Int32.Parse(model["taxtype"]);
            therapistLevel = model["therapistlevel"];
            therapistType = Int32.Parse(model["therapistType"]);

            address1 = model["address1"];
            address2 = model["address2"];
            city = Int32.Parse(model["city"]);
            state = Int32.Parse(model["state"]);
            zip = model["zip"];
            country = 0;
            companyName = model["companyName"];
            education = model["education"];
            fein = model["fein"];
            npi = model["npi"];
            license = model["license"];
            provider = model["provider"];
            firstName = model["firstName"];
            lastName = model["lastName"];
            middleName = model["middleName"];
            gender = Int32.Parse(model["gender"]);
            birthdate = model["birthdate"].MutedDateTime();
            languages = model["languages"];
            ssn = model["ssn"];
            hiredate = model["hiredate"].MutedDateTime();
            email = model["email"];
            phone = model["phone"];
            status = Int32.Parse(model["status"]);
            supervisorId = Int32.Parse(model["supervisor"]);
            terminationdate = model["terminationdate"].MutedDateTime();


        }
    }
}