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
        /// <summary>
        /// lists all the problem
        /// </summary>
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
        ///
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
        /// <summary>
        /// creates a new problem and stores it in the database
        /// </summary>
        /// <param name="ProblemName">name of the problem</param>
        /// <param name="ProblemGrade">grade of the problem</param>
        /// <returns></returns>
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
        /// <summary>
        /// displays the edit page
        /// </summary>
        /// <param name="id">edits for this problem id</param>
        /// <returns></returns>
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
        /// <summary>
        /// updates the database with the updated problem
        /// </summary>
        /// <param name="problem">problem that has been edited</param>
        /// <returns></returns>
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
        /// <summary>
        /// displays the delete confirm page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// deletes problem from database
        /// </summary>
        /// <param name="id">delets the problem with this problemid</param>
        /// <returns></returns>
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
