//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECRWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeePhone
    {
        public int EmployeePhoneId { get; set; }
        public int EmployeeId { get; set; }
        public short PhoneTypeId { get; set; }
        public string CountryCode { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public byte RecordStatusId { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
