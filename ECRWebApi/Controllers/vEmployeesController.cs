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
    public class vEmployeesController : ApiController
    {
        private CalECREntities db = new CalECREntities();

        // GET: api/vEmployees
        public IQueryable<vEmployee> GetvEmployee()
        {
            return db.vEmployee;
        }

        // GET: api/vEmployees/5
        [ResponseType(typeof(vEmployee))]
        public IHttpActionResult GetvEmployee(string id)
        {
            vEmployee vEmployee = db.vEmployee.Find(id);
            if (vEmployee == null)
            {
                return NotFound();
            }

            return Ok(vEmployee);
        }

        // PUT: api/vEmployees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutvEmployee(string id, vEmployee vEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vEmployee.FirstName)
            {
                return BadRequest();
            }

            db.Entry(vEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/vEmployees
        [ResponseType(typeof(vEmployee))]
        public IHttpActionResult PostvEmployee(vEmployee vEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vEmployee.Add(vEmployee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (vEmployeeExists(vEmployee.FirstName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vEmployee.FirstName }, vEmployee);
        }

        // DELETE: api/vEmployees/5
        [ResponseType(typeof(vEmployee))]
        public IHttpActionResult DeletevEmployee(string id)
        {
            vEmployee vEmployee = db.vEmployee.Find(id);
            if (vEmployee == null)
            {
                return NotFound();
            }

            db.vEmployee.Remove(vEmployee);
            db.SaveChanges();

            return Ok(vEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vEmployeeExists(string id)
        {
            return db.vEmployee.Count(e => e.FirstName == id) > 0;
        }
    }
}