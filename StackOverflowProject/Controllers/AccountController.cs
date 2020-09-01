using Microsoft.SqlServer.Server;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.SessionState;
using StackOverflowProject.CustomFilters;

namespace StackOverflowProject.Controllers
{
    public class AccountController : Controller
    {
        IUsersService us;

        public AccountController(IUsersService us)
        {
            this.us = us;
        }
        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = this.us.InsertUser(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrenUserIsAdmin"] = false;

                return RedirectToAction("Index", "Home");
            }
           
            else
            
            {
                ModelState.AddModelError("Error", "Invalid Data");
                return View();
            }
            
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentIsAdmin"] = uvm.isAdmin;

                    if (uvm.isAdmin)
                    {
                        return RedirectToRoute(new { area = "admin", Controller = "AdminHome", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("index", "Home");
                    }
                }

                else
                {
                    ModelState.AddModelError("Error", "User Not Found");
                    return View(lvm);
                    
                }
                
            }

            else
            {
                ModelState.AddModelError("Error", "Invalide data");
                return View(lvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("index", "Home");
        }

        [UserAuthorizationFilter]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm=this.us.GetUsersByUserID(uid);
            EditUserDetailsViewModel euvm = new EditUserDetailsViewModel() { Name = uvm.Name, Email = uvm.Email, Mobile = uvm.Mobile, UserID=uvm.UserID };
            return View(euvm); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
                Session["CurrentUserEmail"] = eudvm.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "Please Enter Valid data");
                return View(eudvm);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.us.GetUsersByUserID(uid);
            EditUserPasswordViewModel eupvm = new EditUserPasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", UserID=uvm.UserID };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ChangePassword(EditUserPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserPassword(eupvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Password and confirm Password");
                return View(eupvm);
            }
        }
    }
}