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
        private ProblemDataController controller = new ProblemDataController();
        public ActionResult List()
        {
            Debug.WriteLine("List method called.");
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

        // GET: Problem/New
        public ActionResult CreateProblem()
        {
            return View();
        }

        // POST: Problem/Create
        [HttpPost]
        public ActionResult CreateProblem(string ProblemName, string ProblemGrade)
        {
            try
            {
                Problem problem = new Problem();
                problem.ProblemName = ProblemName;
                problem.ProblemGrade = ProblemGrade;

                controller.AddProblem(problem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("List");
        }
        // GET: Problem/Edit/{id}
        public ActionResult Edit(int id)
        {
            try
            {
                var problem = controller.FindProblem(id);
                return View(problem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Problem problem)
        {
            if (!ModelState.IsValid)
            {
                return View(problem);
            }

            try
            {
                controller.UpdateProblem(problem);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Problem/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                Problem problem = controller.FindProblem(id);
                return View(problem);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        // POST: Problem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("Delete method called.");
            try
            {
                controller.DeleteProblem(id);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
