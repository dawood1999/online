using DigitalPhotoPrintingEproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DigitalPhotoPrintingEproject.Controllers
{
    public class RegisterLoginController : Controller
    {
        DigitalPrintingDBEntities1 db = new DigitalPrintingDBEntities1();
        // GET: RegisterLogin
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserTable ur)
        {
            if (ModelState.IsValid)
            {
                ur.U_Role = "User";
                var checkemail = db.UserTable.Where(a => a.U_Email == ur.U_Email).FirstOrDefault();
                var checkPass = db.UserTable.Where(a => a.U_Password == ur.U_Password).FirstOrDefault();
                if (checkemail != null)
                {
                    ViewBag.msg1 = "Email Already Exist";
                }
                else if (checkPass != null)
                {
                    ViewBag.msg2 = "PassWord Already Exist";
                }
                else
                {
                    db.UserTable.Add(ur);
                    db.SaveChanges();
                    return RedirectToAction("login");
                }
            }
            else
            {
                ViewBag.msg1 = "Invalid Credentials";
            }
            return View();
        }


        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(UserTable ul,string ReturnUrl)
        {
            var log = db.UserTable.Where(a => a.U_Email == ul.U_Email && a.U_Password == ul.U_Password).FirstOrDefault();

            if (log != null)
            {
                FormsAuthentication.SetAuthCookie(log.U_Name, false);
                Session["userId"] = log.U_Id;
                Session["UserName"] = log.U_Name;
                Session["role"] = log.U_Role;
                if (Session["UserName"] != null)
            {
                if (log.U_Role == "Admin")
                {
                 if(ReturnUrl != null)
                 {
                     return Redirect(ReturnUrl);
                 }
               else
                    {
                        return RedirectToAction("UserView", "Admin");
                    }
                 
                }
                else if (log.U_Role == "User")
                {
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
            }
          
         }
           
  
            return RedirectToAction("login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("login", "RegisterLogin");
        }


    }
}