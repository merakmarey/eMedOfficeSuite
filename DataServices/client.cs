//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class client
    {
        public int clientId { get; set; }
        public Nullable<int> directorId { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public Nullable<int> genderId { get; set; }
        public string ssn { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public Nullable<int> cityId { get; set; }
        public Nullable<int> stateId { get; set; }
        public string zip { get; set; }
        public string primaryPayerId { get; set; }
        public Nullable<decimal> InsuraceCopayment { get; set; }
        public Nullable<int> relationshipToPatientId { get; set; }
        public string insuranceId { get; set; }
        public string insuredFirstName { get; set; }
        public string insuredMiddleName { get; set; }
        public string insuredLastName { get; set; }
        public Nullable<int> primaryInsuredGender { get; set; }
        public string insuredAddress1 { get; set; }
        public string insuredAddress2 { get; set; }
        public Nullable<int> insuredCityId { get; set; }
        public Nullable<int> insuredStateId { get; set; }
        public string insuredZip { get; set; }
        public Nullable<System.DateTime> primaryInsuredBirthdate { get; set; }
        public string insuredPhone { get; set; }
        public string secondaryPayerId { get; set; }
        public string secondaryInsuranceId { get; set; }
        public Nullable<decimal> secondaryCopayment { get; set; }
        public Nullable<int> secondaryRelatioshipToPatient { get; set; }
        public string secondaryFirstName { get; set; }
        public string secondaryMiddleName { get; set; }
        public string secondayLastName { get; set; }
        public Nullable<int> secondaryInsuredGender { get; set; }
        public string secondaryInsuredAddress1 { get; set; }
        public string secondaryInsuredAddress2 { get; set; }
        public Nullable<int> secondaryInsuredCityId { get; set; }
        public Nullable<int> secondaryInsuredStateId { get; set; }
        public string secondaryInsuredZip { get; set; }
        public Nullable<System.DateTime> secondaryInsuredBirthdate { get; set; }
        public string secondaryInsuredPhone { get; set; }
        public string clientDiagnosedCodes { get; set; }
        public string referringPhysicianNPI { get; set; }
        public string referringProviderMedicaid { get; set; }
        public string referringPhysicianFirstName { get; set; }
        public string referringPhysicianMiddleName { get; set; }
        public string referringPhysicianLastName { get; set; }
        public string referringPhysicianPhone { get; set; }
        public string referringPhysicianEmail { get; set; }
        public Nullable<System.DateTime> referalExpirationDate { get; set; }
        public string mdLicenseNumber { get; set; }
        public Nullable<int> serviceLocationId { get; set; }
        public Nullable<System.DateTime> startOfServiceDate { get; set; }
        public Nullable<System.DateTime> initialAssesmentDate { get; set; }
        public Nullable<System.DateTime> initialBASPDate { get; set; }
        public Nullable<System.DateTime> coordinatorApprovalDate { get; set; }
        public string cordinatorEmail { get; set; }
        public string coordinatorPhone { get; set; }
        public Nullable<System.DateTime> supportPlanDate { get; set; }
        public Nullable<decimal> weeklyAnalystHours { get; set; }
        public Nullable<decimal> weeklyTechnicianHours { get; set; }
        public Nullable<System.DateTime> terminationDate { get; set; }
    }
}