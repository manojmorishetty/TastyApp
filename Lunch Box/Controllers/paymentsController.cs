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

namespace Lunch_Box.Controllers
{
    public class paymentsController : Controller
    {
        private lunchboxEntities db = new lunchboxEntities();

        // GET: payments
        public async Task<ActionResult> Index()
        {
            var payments = db.payments.Include(p => p.discount).Include(p => p.order);
            return View(await payments.ToListAsync());
        }

        // GET: payments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = await db.payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: payments/Create
        public ActionResult Create()
        {
            ViewBag.discountID = new SelectList(db.discounts, "disId", "couponCode");
            ViewBag.orderID = new SelectList(db.orders, "orderId", "orderStatus");
            return View();
        }

        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "paymentID,nameOnCard,cardType,cardNumber,orderID,expiryDate,cvv,discountID,totalPrice")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.payments.Add(payment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.discountID = new SelectList(db.discounts, "disId", "couponCode", payment.discountID);
            ViewBag.orderID = new SelectList(db.orders, "orderId", "orderStatus", payment.orderID);
            return View(payment);
        }

        // GET: payments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = await db.payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.discountID = new SelectList(db.discounts, "disId", "couponCode", payment.discountID);
            ViewBag.orderID = new SelectList(db.orders, "orderId", "orderStatus", payment.orderID);
            return View(payment);
        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "paymentID,nameOnCard,cardType,cardNumber,orderID,expiryDate,cvv,discountID,totalPrice")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.discountID = new SelectList(db.discounts, "disId", "couponCode", payment.discountID);
            ViewBag.orderID = new SelectList(db.orders, "orderId", "orderStatus", payment.orderID);
            return View(payment);
        }

        // GET: payments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = await db.payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            payment payment = await db.payments.FindAsync(id);
            db.payments.Remove(payment);
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
