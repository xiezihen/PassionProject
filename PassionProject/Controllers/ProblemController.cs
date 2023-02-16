using PassionProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PassionProject.Controllers
{
    public class ProblemController : Controller
    {
        // GET: Problem/List
        public ActionResult List()
        {
            string url = "https://localhost:44387/api/ProblemData/ListProblems";
            HttpClient client = new HttpClient() { };
            HttpResponseMessage res = client.GetAsync(url).Result;

            IEnumerable<Problem> problems = res.Content.ReadAsAsync<IEnumerable<Problem>>().Result;
            //Debug.WriteLine(problems.Count());

            return View(problems);
        }

        // GET: Problem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Problem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Problem/Create
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

        // GET: Problem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Problem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Problem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Problem/Delete/5
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
