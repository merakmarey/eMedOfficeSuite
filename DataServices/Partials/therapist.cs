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
            address1 = model["address1"];
            address2 = model["address2"];
            city = Int32.Parse(model["city"]);
            state = Int32.Parse(model["state"]);
            zip = model["zip"];
            country = 0;

            companyName = model["companyName"];
            education = model["education"];
            email = model["email"];
            fein = model["fein"];
            firstName = model["firstName"];
            lastName = model["lastName"];
            gender = Int32.Parse(model["gender"]);
            birthdate = model["birthdate"].MutedDateTime();
            hiredate = model["hiredate"].MutedDateTime();
            therapistLevel = model["therapistlevel"];
            languages = model["languages"];
            license = model["license"];
            middleName = model["middleName"];
            phone = model["phone"];
            provider = model["provider"];
            ssn = model["ssn"];
            status = Int32.Parse(model["status"]);
            supervisorId = Int32.Parse(model["supervisor"]);
            terminationdate = model["terminationdate"].MutedDateTime();
            therapistType = Int32.Parse(model["therapistType"]);
        }
    }
}