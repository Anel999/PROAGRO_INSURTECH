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
using ProAgroAPIREST.Models;

namespace ProAgroAPIREST.Controllers
{
    public class PermisosController : ApiController
    {
        private ProAgroEntities db = new ProAgroEntities();

        // GET: api/Permisos
        public IQueryable<Permisos> GetPermisos()
        {
            return db.Permisos;
        }

        // GET: api/Permisos/5
        [ResponseType(typeof(Permisos))]
        public IHttpActionResult GetPermisos(int id)
        {
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return NotFound();
            }

            return Ok(permisos);
        }

        // PUT: api/Permisos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermisos(int id, Permisos permisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permisos.idPermiso)
            {
                return BadRequest();
            }

            db.Entry(permisos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisosExists(id))
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

        // POST: api/Permisos
        [ResponseType(typeof(Permisos))]
        public IHttpActionResult PostPermisos(Permisos permisos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permisos.Add(permisos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permisos.idPermiso }, permisos);
        }

        // DELETE: api/Permisos/5
        [ResponseType(typeof(Permisos))]
        public IHttpActionResult DeletePermisos(int id)
        {
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return NotFound();
            }

            db.Permisos.Remove(permisos);
            db.SaveChanges();

            return Ok(permisos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermisosExists(int id)
        {
            return db.Permisos.Count(e => e.idPermiso == id) > 0;
        }
    }
}