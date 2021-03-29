using DigitalPhotoPrintingEproject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalPhotoPrintingEproject.Controllers
{
    
    public class HomeController : Controller
    {
        DigitalPrintingDBEntities1 db = new DigitalPrintingDBEntities1();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
       
        public ActionResult Gellery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Gellery(GelleryTable stu)
        {

            if (ModelState.IsValid)
            {

                string imagename = Path.GetFileNameWithoutExtension(stu.Imageayi.FileName);
                string extension = Path.GetExtension(stu.Imageayi.FileName);
                HttpPostedFileBase serverimage = stu.Imageayi;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".jpg")
                {
                    if (size <= 2000000)
                    {
                        imagename = imagename + extension;
                        stu.mg_Image = "~/Images/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/Images/"), imagename);
                        stu.Imageayi.SaveAs(imagename);
                        stu.U_Id = (int) Session["userId"];
                        db.GelleryTable.Add(stu);
                        db.SaveChanges();
                        return RedirectToAction("showImage");
                    }
                    else
                    {
                        ViewBag.msg = "<script>alert('invalid error')</script>";
                    }
                }
                else
                {
                    ViewBag.msg = "<script>alert('Wrong Extension')</script>";
                }
            }
                return View();
            
        }
        public ActionResult showImage()
        {
            var userId = (int)Session["userId"];
            return View(db.GelleryTable.Where(t => t.U_Id == userId).ToList());

        }


        
    }
}