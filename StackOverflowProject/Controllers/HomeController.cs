using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionsService qs;
        public HomeController(IQuestionsService qs)
        {
            this.qs = qs;
        }
        public ActionResult Index()
        {
            List<QuestionViewModel> questions = this.qs.GetQuestions().Take(10).ToList();
            return View(questions);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}