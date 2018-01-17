using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECRWebApi.Models
{

    public class Contacts
    {
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
    }

    public class EmployeeContacts
    {
        public string Ueid { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
    }
    public class EmployeesContacts
    {
        public List<EmployeeContacts> EmployeeContacts { get; set; }
        public IEnumerator<EmployeeContacts> GetEnumerator()
        {
            return EmployeeContacts.GetEnumerator();
        }
    }
    public class EmployeePosition
    {
        public string Tenure { get; set; }
        public string Timebase { get; set; }
        public string SafetyCode { get; set; }
        public string AgencyCode { get; set; }
        public string ClassificationTitle { get; set; }
        public string ClassCode { get; set; }
    }
    public class EmployeeInfo
    {
        public string UniqueEmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string AgencyCode { get; set; }
        public  ICollection<EmployeePosition> EmployeePosition { get; set; }
    }
    //public class EmployeeEmailDto
    //{
    //    public int EmployeeId { get; set; }
    //    public byte EmailTypeId { get; set; }
    //    public string Email { get; set; }
    //    public byte RecordStatusId { get; set; }
    //    public System.DateTime CreateDate { get; set; }

    //}
}