using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalPhotoPrintingEproject.Models;
using System.IO;

namespace DigitalPhotoPrintingEproject.Controllers
{
    public class FrameTablesController : Controller
    {
        private DigitalPrintingDBEntities1 db = new DigitalPrintingDBEntities1();

        // GET: FrameTables
        public ActionResult Index()
        {
            return View(db.FrameTable.ToList());
        }

        // GET: FrameTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrameTable frameTable = db.FrameTable.Find(id);
            if (frameTable == null)
            {
                return HttpNotFound();
            }
            return View(frameTable);
        }

        // GET: FrameTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrameTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(FrameTable stu)
        {

            if (ModelState.IsValid)
            {
                string imagename = Path.GetFileNameWithoutExtension(stu.Imageayi.FileName);
                string extension = Path.GetExtension(stu.Imageayi.FileName);
                HttpPostedFileBase serverimage = stu.Imageayi;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".png")
                {
                    if (size <= 2000000)
                    {
                        imagename = imagename + extension;
                        stu.f_Image = "~/Images/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/Images/"), imagename);
                        stu.Imageayi.SaveAs(imagename);
                        db.FrameTable.Add(stu);
                        db.SaveChanges();
                        ModelState.Clear();
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

        // GET: FrameTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrameTable frameTable = db.FrameTable.Find(id);
            if (frameTable == null)
            {
                return HttpNotFound();
            }
            return View(frameTable);
        }

        // POST: FrameTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "f_Id,f_Image,f_Size,f_Price,f_Type,f_Name")] FrameTable frameTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frameTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frameTable);
        }

        // GET: FrameTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrameTable frameTable = db.FrameTable.Find(id);
            if (frameTable == null)
            {
                return HttpNotFound();
            }
            return View(frameTable);
        }

        // POST: FrameTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FrameTable frameTable = db.FrameTable.Find(id);
            db.FrameTable.Remove(frameTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
