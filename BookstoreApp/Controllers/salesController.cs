using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookstoreApp.Models;
using BookstoreApp.Models.ViewModels;

namespace BookstoreApp.Controllers
{
    public class salesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: sales
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.store).Include(s => s.title);
            return View(sales.ToList());
        }

        // GET: sales
        public ActionResult OrderSearch()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult OrderSearch([Bind(Include = "nameFrom,nameTo,dateFrom,dateTo")] OrderSearchFormViewModel searchFormData)
        {
            var nameFrom = searchFormData.nameFrom;
            var nameTo = searchFormData.nameTo;
            var dateFrom = searchFormData.dateFrom;
            var dateTo = searchFormData.dateTo;

            var orders = from order in db.sales
                         from book in db.titles
                         from store in db.stores
                         where order.ord_date > dateFrom
                         where order.ord_date < dateTo
                         where book.title_id == order.title_id
                         where store.stor_id == order.stor_id
                         //where store.stor_name[0] > nameFrom
                         //where store.stor_name[0] < nameTo
                         select new
                         {
                             orderId = order.ord_num,
                             storeName = store.stor_name,
                             bookName = book.title1
                         };

            List<OrderSearchResultViewModel> results = new List<OrderSearchResultViewModel>();
            foreach (var ord in orders)
            {
                results.Add(new OrderSearchResultViewModel(ord.orderId, ord.storeName, ord.bookName));
            }

            return View("OrderSearchResult",results);


            //var books = (from book in db.titles
            //             where book.pubdate > dateFrom
            //             where book.pubdate < dateTo
            //             orderby book.ytd_sales descending
            //             select book)
            //               .Take(searchFormData.sales);

            //var authors = (from b in books
            //               from author in db.authors
            //               from tauth in db.titleauthors
            //               where tauth.title_id == b.title_id
            //               where tauth.au_id == author.au_id
            //               select author).ToList();

            //return View("Result", authors);
        }

        // GET: sales/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                db.sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            return View(sale);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            return View(sale);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            return View(sale);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale sale = db.sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            sale sale = db.sales.Find(id);
            db.sales.Remove(sale);
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
