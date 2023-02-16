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
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class ProblemDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProblemData/ListProblems
        [HttpGet]
        [Route("api/ProblemData/ListProblems")]
        public IQueryable<Problem> ListProblems()
        {
            return db.Problems;
        }

        // GET: api/ProblemData/FindProblem/5
        [HttpGet]
        [ResponseType(typeof(Problem))]
        [Route("api/ProblemData/FindProblem/{id}")]
        public IHttpActionResult FindProblem(int id)
        {
            Problem problem = db.Problems.Find(id);
            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem);
        }

        // POST: api/ProblemData/UpdateProblem/5
        [Route("api/ProblemData/UpdateProblem/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProblem(int id, Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != problem.ProblemID)
            {
                return BadRequest();
            }

            db.Entry(problem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemExists(id))
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

        // POST: api/ProblemData/AddProblem
        [ResponseType(typeof(Problem))]
        [HttpPost]
        [Route("api/ProblemData/AddProblem")]
        public IHttpActionResult AddProblem(Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Problems.Add(problem);
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/ProblemData/DeleteAnimal/5
        [ResponseType(typeof(Problem))]
        [HttpPost]
        [Route("api/ProblemData/DeleteProblem/{id}")]
        public IHttpActionResult DeleteProblem(int id)
        {
            Problem problem = db.Problems.Find(id);
            if (problem == null)
            {
                return NotFound();
            }

            db.Problems.Remove(problem);
            db.SaveChanges();

            return Ok(problem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProblemExists(int id)
        {
            return db.Problems.Count(e => e.ProblemID == id) > 0;
        }
    }
}