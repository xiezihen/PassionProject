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