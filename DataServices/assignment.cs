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
    
    public partial class assignment
    {
        public int therapistAssignmentId { get; set; }
        public int therapistId { get; set; }
        public int clientId { get; set; }
        public System.DateTime startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
    }
}