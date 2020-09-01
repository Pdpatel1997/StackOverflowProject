using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ViewModels;
using StackOverflowProject.ServiceLayer;
using System.Web.WebSockets;
using StackOverflowProject.CustomFilters;

namespace StackOverflowProject.Controllers
{
    public class QuestionsController : Controller
    {

        IQuestionsService qs;
        ICategoriesService cs;
        IAnswersService asr;

        public QuestionsController(IQuestionsService qs, IAnswersService asr, ICategoriesService cs)
        {
            this.qs = qs;
            this.cs = cs;
            this.asr = asr;
        }



        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;


            if (ModelState.IsValid)
            {
                this.asr.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }

            else
            {
                ModelState.AddModelError("Error", "Please enter Answer");
                QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(navm.QuestionID, navm.UserID);
                return View("View", qvm);
            }
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel eavm)
        {
            if (ModelState.IsValid)
            {
                eavm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.asr.UpdateAnswer(eavm);
                return RedirectToAction("View", new { id = eavm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid data");
                return RedirectToAction("View", new { id = eavm.QuestionID });
            }

        }

        public ActionResult Create(NewQuestionViewModel qvm)
        {
            List<CategoryViewModel> categories = this.cs.GetCategories();
            ViewBag.categories = categories;
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        
        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.ViewsCount = 0;
                qvm.VotesCount = 0;
                qvm.QuestionDateAndTime = DateTime.Now;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.qs.InsertQuestion(qvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Question");
                return View();
            }
        }
    }
}