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
    //public class EmployeeEmailDto
    //{
    //    public int EmployeeId { get; set; }
    //    public byte EmailTypeId { get; set; }
    //    public string Email { get; set; }
    //    public byte RecordStatusId { get; set; }
    //    public System.DateTime CreateDate { get; set; }

    //}
}