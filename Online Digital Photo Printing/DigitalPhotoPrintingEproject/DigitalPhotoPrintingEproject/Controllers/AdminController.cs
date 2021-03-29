using DigitalPhotoPrintingEproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DigitalPhotoPrintingEproject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        DigitalPrintingDBEntities1 db = new DigitalPrintingDBEntities1();
        // GET: Admin
        public ActionResult admin()
        {
            return View();
        }
        public ActionResult UserView()
        {
            ViewData["TutorCount"] = db.UserTable.Count();  
            return View(db.UserTable.ToList());
        }

       // Order Placement


        // GET: OrderTables
        public ActionResult OderAdminView()
        {
            var orderTable = db.OrderTable.Include(o => o.FrameTable).Include(o => o.GelleryTable).Include(o => o.UserTable);
            return View(orderTable.ToList());
        }

        // GET: OrderTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTable.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // POST: OrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTable orderTable = db.OrderTable.Find(id);
            db.OrderTable.Remove(orderTable);
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