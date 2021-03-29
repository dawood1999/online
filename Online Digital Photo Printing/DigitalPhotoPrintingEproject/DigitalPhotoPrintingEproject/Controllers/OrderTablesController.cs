using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalPhotoPrintingEproject.Models;

namespace DigitalPhotoPrintingEproject.Controllers
{
    public class OrderTablesController : Controller
    {
        private DigitalPrintingDBEntities1 db = new DigitalPrintingDBEntities1();

        // GET: OrderTables
        public ActionResult Index()
        {
            var userId = (int)Session["userId"];
            var orderTable = db.OrderTable.Include(o => o.FrameTable).Include(o => o.GelleryTable).Include(o => o.UserTable);
            return View(orderTable.Where(a=>a.U_Id == userId).ToList());
        
            //   var userId = (int)Session["userId"];
       //  return View(db.UserTable.Where(t => t.U_Id == userId).ToList());
        }

        // GET: OrderTables/Details/5
        public ActionResult Details(int? id)
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

        // GET: OrderTables/Create
        public ActionResult Create(int id)
        {
            var data = db.OrderTable.Where(a => a.mg_Id == id).FirstOrDefault();
            ViewBag.f_Id = new SelectList(db.FrameTable, "f_Id", "f_Image");
            ViewBag.mg_Id = new SelectList(db.GelleryTable, "mg_Id", "mg_Image");
            ViewBag.U_Id = new SelectList(db.UserTable, "U_Id", "U_Name");
            return View(data);
        }

        // POST: OrderTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "o_Id,U_Id,mg_Id,f_Id,o_Size_,o_Date,o_Quantity,o_Address,o_Price,o_TotalPrice")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
               FrameTable ff = new FrameTable();
                var price = db.FrameTable.Where(a => a.f_Id == orderTable.f_Id).FirstOrDefault();
                orderTable.o_Price = price.f_Price;         
                orderTable.o_TotalPrice = orderTable.o_Price * orderTable.o_Quantity;
                db.OrderTable.Add(orderTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.f_Id = new SelectList(db.FrameTable, "f_Id", "f_Image", orderTable.f_Id);
            ViewBag.mg_Id = new SelectList(db.GelleryTable, "mg_Id", "mg_Image", orderTable.mg_Id);
            ViewBag.U_Id = new SelectList(db.UserTable, "U_Id", "U_Name", orderTable.U_Id);
            return View(orderTable);
        }

        // GET: OrderTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTable.Find(id);
            if (id == orderTable.mg_Id )
            {
                ViewBag.f_Id = new SelectList(db.FrameTable, "f_Id", "f_Image", orderTable.f_Id);
                ViewBag.mg_Id = new SelectList(db.GelleryTable, "mg_Id", "mg_Image", orderTable.mg_Id);
                ViewBag.U_Id = new SelectList(db.UserTable, "U_Id", "U_Name", orderTable.U_Id);
                return View(orderTable);
            }
            if (orderTable == null)
            {
                return HttpNotFound();
            }
           return HttpNotFound(); 
        }

        // POST: OrderTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "o_Id,U_Id,mg_Id,f_Id,o_Size_,o_Date,o_Quantity,o_Address,o_Price,o_TotalPrice")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.f_Id = new SelectList(db.FrameTable, "f_Id", "f_Image", orderTable.f_Id);
            ViewBag.mg_Id = new SelectList(db.GelleryTable, "mg_Id", "mg_Image", orderTable.mg_Id);
            ViewBag.U_Id = new SelectList(db.UserTable, "U_Id", "U_Name", orderTable.U_Id);
            return View(orderTable);
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
