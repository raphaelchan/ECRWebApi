using System;
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
        /// <response code="200">Success</response>
        /// <response code="204">No Matching Employees</response>
        /// <param name="AgencyCode"></param>
        /// <returns></returns>
        public IQueryable<vEmployeeInfo> GetvEmployeeInfo(string AgencyCode)
        {
            var employeeInfo = db.vEmployeeInfo
                               .Where(t => t.AgencyCode.Equals(AgencyCode));
            return (employeeInfo);
        }

        /// <summary>
        /// Update ECR Phone/Email Batch File
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="207">Partial Updated</response>
        /// <returns></returns>

        public IHttpActionResult PostEmployeesContacts(EmployeesContacts employeesInfo)
        {
            int responseCode = 200;
            string responseBody = "";
            int noMatch = 0;
            int updateFail = 0;
            int totalCnt = 0;
            string euidNotMatch = "";
            string euidSaveFail = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in employeesInfo)
            {
                totalCnt++;
                var er = db.Employee
                        .Where(t => t.UniqueEmployeeId.Equals(item.Ueid)).FirstOrDefault();

                if (er == null)
                {
                    noMatch++;
                    euidNotMatch = euidNotMatch + item.Ueid + ",";
                }
                else
                {
                    if (item.Email != null)
                    {
                        UpdateEmail(er.EmployeeId, item.Email, WORKEMAILTYPE);
                    }
                    if (item.HomePhone != null)
                    {
                        UpdatePhone(er.EmployeeId, item.HomePhone, HOMEPHONETYPE);
                    }
                    if (item.WorkPhone != null)
                    {
                        UpdatePhone(er.EmployeeId, item.WorkPhone, WORKPHONETYPE);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        updateFail++;
                        euidSaveFail = euidSaveFail + item.Ueid + ",";
                    }
                }
            }
            if (noMatch > 0 || updateFail > 0)
            {
                responseCode = 207;
                responseBody = "Total " + (totalCnt - noMatch - updateFail) + " UEIDs updated. "
                    + noMatch + " Invalid UEIDs:" + euidNotMatch.TrimEnd(',') + "."
                    + updateFail + " UEIDs failed to update due to DB error:" + euidSaveFail.TrimEnd(',') + ".";
            }
            else
            {
                responseBody = totalCnt + " UEID updated.";
                responseCode = 200;
            }
            return new System.Web.Http.Results.ResponseMessageResult(
                        Request.CreateErrorResponse(
                            (HttpStatusCode)responseCode,
                            new HttpError(responseBody)
                        )
                );
            //db.EmployeeEmail.Add(employeeEmail);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = employeeEmail.EmployeeEmailId }, employeeEmail);
        }
        /// <summary>
        /// Update Employee phones and email
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">Invalid UEID</response>
        /// <response code="500">Database Error</response>
        /// <param name="UEID"></param>
        /// <returns></returns>
        public IHttpActionResult PutContacts(string ueid, Contacts contacts)
        {
            int responseCode = 200;
            string responseBody = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var er = db.Employee
                .Where(t => t.UniqueEmployeeId.Equals(ueid)).FirstOrDefault();

            if (er == null)
            {
                responseCode = 404;
                responseBody = "Invalid UEID";
            }
            else
            {
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
                    responseCode = 500;
                    responseBody = "Unable to save changes";
                }
            }
            return new System.Web.Http.Results.ResponseMessageResult(
            Request.CreateErrorResponse(
                (HttpStatusCode)responseCode,
                new HttpError(responseBody)
                        )
                );
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

        //Update email address
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