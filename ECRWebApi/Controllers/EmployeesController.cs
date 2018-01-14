﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ECRWebApi.Models;

namespace ECRWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private CalECREntities1 db = new CalECREntities1();
        private readonly byte RECORDSTATUS = 1;
        private readonly byte HOMEPHONETYPE = 1;
        private readonly byte WORKEMAILTYPE = 2;
        private readonly byte WORKPHONETYPE = 2;
        

        // GET: api/Employees
        /// <summary>
        /// Get ECR Employee data by an AgencyCode
        /// </summary>
        /// <param name="AgencyCode"></param>
        /// <returns></returns>
        public IQueryable<vEmployeeInfo> GetvEmployeeInfo(string AgencyCode)
        {
            var employeeInfo = db.vEmployeeInfo
                               .Where(t => t.AgencyCode.Equals(AgencyCode));
            return (employeeInfo);
        }

        /// <summary>
        /// Update Employee phones and email
        /// </summary>
        /// <param name="UEID"></param>
        /// <returns></returns>
        public IHttpActionResult PutEmployee(string ueid, ECRContactClass contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var er = db.Employee
                .Where(t => t.UniqueEmployeeId.Equals(ueid)).FirstOrDefault();

            if (er == null)
            {
                return NotFound();
            }

            if (contacts.Email != null)
            {
                UpdateEmail(er.EmployeeId, contacts.Email, WORKEMAILTYPE);
            }

            if (contacts.HomePhone != null)
            { 
                UpdatePhone(er.EmployeeId, contacts.HomePhone, HOMEPHONETYPE);
            }

            if (contacts.WorkPhone != null)
            {
                UpdatePhone(er.EmployeeId, contacts.WorkPhone, WORKPHONETYPE);
            }

            //db.Entry(contacts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!EmployeeEmailExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }


            return Ok();
        }

        private void UpdatePhone(int employeeId, string phoneNum, byte phoneType)
        {
            var phonerec = db.EmployeePhone
            .Where(t => t.EmployeeId.Equals(employeeId) && t.PhoneTypeId == phoneType).FirstOrDefault();
            if (phonerec == null)
            {
                EmployeePhone phone = new EmployeePhone();
                phone.EmployeeId = employeeId;
                phone.Number = phoneNum;
                phone.CreateDate = DateTime.Now;
                phone.RecordStatusId = RECORDSTATUS;
                phone.PhoneTypeId = phoneType;
                db.EmployeePhone.Add(phone);
            }
            else
            {
                phonerec.Number = phoneNum;
                phonerec.CreateDate = DateTime.Now;
            }
        }
        private void UpdateEmail(int employeeId, string emailAdr, byte emailType)
        {
            var mailrec = db.EmployeeEmail
            .Where(t => t.EmployeeId.Equals(employeeId) && t.EmailTypeId == emailType).FirstOrDefault();
            if (mailrec == null)
            {
                EmployeeEmail email = new EmployeeEmail();
                email.EmployeeId = employeeId;
                email.Email = emailAdr;
                email.CreateDate = DateTime.Now;
                email.RecordStatusId = RECORDSTATUS;
                email.EmailTypeId = emailType;
                db.EmployeeEmail.Add(email);
            }
            else
            {
                mailrec.Email = emailAdr;
                mailrec.CreateDate = DateTime.Now;
            }
        }


    }
}