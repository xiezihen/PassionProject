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
    public class HoldDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns a list of all holds in the database.
        /// </summary>
        /// <returns>An IEnumerable of HoldDto objects representing each hold.</returns>
        // GET: api/HoldData/ListHolds
        [HttpGet]
        [Route("api/HoldData/ListHolds")]
        public IEnumerable<HoldDto> ListHolds()
        {
            List < Hold >  holds = db.Holds.ToList();
            List<HoldDto> holdsDto = new List<HoldDto>();
            holds.ForEach(a => holdsDto.Add(new HoldDto()
            {
                HoldID = a.HoldID,
                PositionX = a.PositionX,
                PositionY = a.PositionY
            })) ;

            return holdsDto;
        }
        /// <summary>
        /// Returns a list of all holds associated with the specified problem.
        /// </summary>
        /// <param name="problemId">The ID of the problem to filter holds by.</param>
        /// <returns>An IEnumerable of HoldDto objects representing each hold associated with the specified problem.</returns>
        // GET: api/HoldData/ListHoldsByProblemID/5
        [HttpGet]
        [Route("api/HoldData/ListHoldsByProblemID/{problemId}")]
        public IEnumerable<HoldDto> ListHoldsByProblemID(int problemId)
        {
            List<Hold> holds = db.Holds.Where(h => h.ProblemID == problemId).ToList();
            List<HoldDto> holdsDto = new List<HoldDto>();

            holds.ForEach(a => holdsDto.Add(new HoldDto()
            {
                HoldID = a.HoldID,
                PositionX = a.PositionX,
                PositionY = a.PositionY
            }));

            return holdsDto;
        }

        /// <summary>
        /// Finds the hold with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the hold to find.</param>
        /// <returns>An IHttpActionResult containing the Hold object with the specified ID, or NotFound if no such hold exists.</returns>
        // GET: api/HoldData/FindHold/5
        [Route("api/HoldData/FindHold/{id}")]
        [ResponseType(typeof(Hold))]
        [HttpGet]
        public IHttpActionResult FindHold(int id)
        {
            Hold hold = db.Holds.Find(id);
            if (hold == null)
            {
                return NotFound();
            }

            return Ok(hold);
        }
        /// <summary>
        /// Updates the hold with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the hold to update.</param>
        /// <param name="hold">The updated Hold object.</param>
        /// <returns>An IHttpActionResult indicating success or failure of the update.</returns>
        // PUT: api/HoldData/UpdateHold/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/HoldData/UpdateHold/{id}")]
        public IHttpActionResult UpdateHold(int id, Hold hold)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hold.HoldID)
            {
                return BadRequest();
            }

            db.Entry(hold).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoldExists(id))
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
        /// Updates a list of Holds.
        /// </summary>
        /// <param name="holds">The list of Holds to update.</param>
        /// <returns>Returns an HTTP result indicating success or failure.</returns>
        // PUT: api/HoldData/UpdateHolds
        [HttpPut]
        [Route("api/HoldData/UpdateHolds")]
        public IHttpActionResult UpdateHolds(List<Hold> holds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Hold hold in holds)
            {
                if (!HoldExists(hold.HoldID))
                {
                    return NotFound();
                }

                db.Entry(hold).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Adds a new Hold.
        /// </summary>
        /// <param name="hold">The Hold to add.</param>
        /// <returns>Returns an HTTP result indicating success or failure.</returns>
        // POST: api/HoldData/AddHold
        [ResponseType(typeof(Hold))]
        [HttpPost]
        [Route("api/HoldData/AddHold")]
        public IHttpActionResult AddHold(Hold hold)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Holds.Add(hold);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Deletes a Hold with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Hold to delete.</param>
        /// <returns>Returns an HTTP result indicating success or failure.</returns>
        // DELETE: api/HoldData/DeleteHold/5
        [ResponseType(typeof(Hold))]
        [HttpPost]
        [Route("api/HoldData/DeleteHold/{id}")]
        public IHttpActionResult DeleteHold(int id)
        {
            Hold hold = db.Holds.Find(id);
            if (hold == null)
            {
                return NotFound();
            }

            db.Holds.Remove(hold);
            db.SaveChanges();

            return Ok(hold);
        }
        /// <summary>
        /// Deletes a list of Holds with the specified IDs.
        /// </summary>
        /// <param name="holdIDs">The list of IDs of the Holds to delete.</param>
        /// <returns>Returns an HTTP result indicating success or failure.</returns>
        // DELETE: api/HoldData/DeleteHolds
        [HttpDelete]
        [Route("api/HoldData/DeleteHolds")]
        public IHttpActionResult DeleteHolds(List<int> holdIDs)
        {
            foreach (int holdID in holdIDs)
            {
                Hold hold = db.Holds.Find(holdID);
                if (hold == null)
                {
                    return NotFound();
                }
                db.Holds.Remove(hold);
            }
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Adds a list of Holds.
        /// </summary>
        /// <param name="holds">The list of Holds to add.</param>
        /// <returns>Returns an HTTP result indicating success or failure.</returns>
        // POST: api/HoldData/AddHolds
        [ResponseType(typeof(Hold))]
        [HttpPost]
        [Route("api/HoldData/AddHolds/")]
        public IHttpActionResult AddHolds(List<Hold> holds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var hold in holds)
            {
                db.Holds.Add(hold);
            }

            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoldExists(int id)
        {
            return db.Holds.Count(e => e.HoldID == id) > 0;
        }
    }
}