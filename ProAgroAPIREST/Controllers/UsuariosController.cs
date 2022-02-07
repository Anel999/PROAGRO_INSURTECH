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
    public class UsuariosController : ApiController
    {
        private ProAgroEntities db = new ProAgroEntities();   //+++aqui esta mi BD

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuario()
        {
            return db.Usuario;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // db.Usuario.Add(usuario);

            try
            {
                // db.SaveChanges();
                using (db)
                {
                    var lst = from d in db.Usuario
                              where d.idUsuario == usuario.idUsuario && d.Contraseña == usuario.Contraseña
                              select d;
                    if (lst.Count() > 0)
                    {
                        return CreatedAtRoute("DefaultApi", new { id = usuario.idUsuario }, usuario);
                    }
                    else
                    {
                        return Conflict();
                    }
                }
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.idUsuario))
                {
                    // return Conflict();
                    return CreatedAtRoute("DefaultApi", new { id = usuario.idUsuario }, usuario);
                }
                else
                {
                    throw;
                }
            }

        //return CreatedAtRoute("DefaultApi", new { id = usuario.idUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuario.Count(e => e.idUsuario == id) > 0;
        }
    }
}