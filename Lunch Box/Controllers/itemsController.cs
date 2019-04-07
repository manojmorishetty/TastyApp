using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lunch_Box;
using System.IO;

namespace Lunch_Box.Controllers
{
    public class itemsController : Controller
    {
        private lunchboxEntities db = new lunchboxEntities();

        // GET: items
        public ActionResult Main()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            return View(await db.items.ToListAsync());
        }

        // GET: items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = await db.items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "itemId,snackType,snackState,basePrice,quantity,unit,img_src,ImageFile")] item item)
        {
            if (ModelState.IsValid)
            {
                    string fileName = item.itemId.ToString();
                    string extension=Path.GetExtension(item.ImageFile.FileName);
                    fileName += extension;
                    item.img_src = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    item.ImageFile.SaveAs(fileName);
                db.items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(item);
        }

        // GET: items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = await db.items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "itemId,snackType,snackState,basePrice,quantity,unit")] item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string fullPath = Request.MapPath("~/Content/Images/" + id+".png");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            item item = await db.items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            item item = await db.items.FindAsync(id);
            db.items.Remove(item);
            await db.SaveChangesAsync();
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
