using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PassionProject.Controllers
{
    public class HoldsController : Controller
    {
        // GET: Holds/ProblemId/{problemId}
        public ActionResult ProblemId(int id)
        {
            string urlHolds = $"https://localhost:44387/api/HoldData/ListHoldsByProblemID/{id}";
            HttpClient client = new HttpClient();
            HttpResponseMessage resHolds = client.GetAsync(urlHolds).Result;
            string urlProblem = $"https://localhost:44387/api/ProblemData/FindProblem/{id}";
            HttpResponseMessage resProblem = client.GetAsync(urlProblem).Result;

            if (resHolds.IsSuccessStatusCode)
            {
                var holds = resHolds.Content.ReadAsAsync<IEnumerable<Hold>>().Result;
                var problem = resProblem.Content.ReadAsAsync<Problem>().Result;
                var model = new ProblemHoldsViewModel();
                model.Holds = holds;
                model.Problem = problem;

                return View(model);
            }

            return View();
        }

        // GET: Holds/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Holds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holds/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Holds/Edit/5
        public ActionResult EditHolds(int id)
        {
            string urlHolds = $"https://localhost:44387/api/HoldData/ListHoldsByProblemID/{id}";
            HttpClient client = new HttpClient();
            HttpResponseMessage resHolds = client.GetAsync(urlHolds).Result;
            string urlProblem = $"https://localhost:44387/api/ProblemData/FindProblem/{id}";
            HttpResponseMessage resProblem = client.GetAsync(urlProblem).Result;

            if (resHolds.IsSuccessStatusCode)
            {
                var holds = resHolds.Content.ReadAsAsync<IEnumerable<Hold>>().Result;
                var problem = resProblem.Content.ReadAsAsync<Problem>().Result;
                var model = new ProblemHoldsViewModel();
                model.Holds = holds;
                model.Problem = problem;

                return View(model);
            }

            return View();
        }

        // POST: Holds/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IEnumerable<Hold> holds)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Holds/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Holds/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
