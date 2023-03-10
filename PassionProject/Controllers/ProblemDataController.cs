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
        /// <summary>
        /// Gets a list of all problems.
        /// </summary>
        /// <returns>An IQueryable of Problem objects.</returns>
        // GET: api/ProblemData/ListProblems
        [HttpGet]
        [Route("api/ProblemData/ListProblems")]
        public IQueryable<Problem> ListProblems()
        {
            return db.Problems;
        }
        /// <summary>
        /// Finds a specific problem by ID.
        /// </summary>
        /// <param name="id">The ID of the problem to find.</param>
        /// <returns>The Problem object with the specified ID.</returns>
        // GET: api/ProblemData/FindProblem/5
        [HttpGet]
        [ResponseType(typeof(Problem))]
        [Route("api/ProblemData/FindProblem/{id}")]
        public Problem FindProblem(int id)
        {
            Problem problem = db.Problems.Find(id);
            return problem;
        }
        /// <summary>
        /// Updates an existing problem.
        /// </summary>
        /// <param name="problem">The problem object to update.</param>
        /// <returns>An IHttpActionResult indicating the success of the operation.</returns>
        // POST: api/ProblemData/UpdateProblem/5
        [Route("api/ProblemData/UpdateProblem/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProblem(Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (problem.ProblemID != problem.ProblemID)
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
                if (!ProblemExists(problem.ProblemID))
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
        /// <summary>
        /// Adds a new problem.
        /// </summary>
        /// <param name="problem">The problem object to add.</param>
        /// <returns>An IHttpActionResult indicating the success of the operation.</returns>
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
        /// <summary>
        /// Deletes a problem.
        /// </summary>
        /// <param name="id">The ID of the problem to delete.</param>
        /// <returns>An IHttpActionResult indicating the success of the operation.</returns>
        // DELETE: api/ProblemData/DeleteProblem/5
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