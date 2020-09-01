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
        ICategoriesService cs;
        public HomeController(IQuestionsService qs,ICategoriesService cs)
        {
            this.qs = qs;
            this.cs = cs;   
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

        public ActionResult Categories()
        {
            List<CategoryViewModel> cvm = this.cs.GetCategories();
            return View(cvm);
        }
        [Route("AllQuestions")]
        public ActionResult AllQuestions()
        {
            List<QuestionViewModel> qvm = this.qs.GetQuestions();
            return View(qvm);
        }

        public ActionResult Search(string str)
        {
            List<QuestionViewModel> qvm = this.qs.GetQuestions().Where(s => s.QuestionName.ToLower().Contains(str.ToLower()) ||
            s.category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            ViewBag.str = str;
                
            return View(qvm);
        }
    }
}