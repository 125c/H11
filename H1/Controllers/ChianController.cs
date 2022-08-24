using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using H1.Models;

namespace H1.Controllers
{
    public class ChainController : Controller
    {
        private HerLin0030Entitieschain db = new HerLin0030Entitieschain();

        // GET: Members
        public ActionResult Index()
        {
            //var chain = db.Chain.Include(m => m.GetChain());
            var chain = db.Chain;
            return View(chain);
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chain chain = db.Chain.Find(id);
            if (chain == null)
            {
                return HttpNotFound();
            }
            return View(chain);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.countyID = db.Chain.ToList();
            ViewBag.SaleStateID = db.Chain.ToList();

            //ViewBag.MemberType = new SelectList(db.MemberType, "MemberTypeIdentify", "MemberType1");
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChainIdentify,ChainName")] Chain chain)
        {
            if (ModelState.IsValid)
            {
                db.Chain.Add(chain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ChainID = new SelectList(db.Chain, "ChainIdentify", "ChainName", chain.Chain);
            ViewBag.ChainID = db.Chain.ToList();
            ViewBag.ChainName = db.Chain.ToList();
            return View();
        }

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chain chain = db.Chain.Find(id);
            if (chain == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MemberType = new SelectList(db.MemberType, "MemberTypeIdentify", "MemberType1", member.MemberType);
            return View(chain);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChainIdentify,ChainName")] Chain chain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.MemberType = new SelectList(db.MemberType, "MemberTypeIdentify", "MemberType1", member.MemberType);
            return View(chain);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Chain chain = db.Chain.Find(id);
            if (chain == null)
            {
                return HttpNotFound();
            }
            return View(chain);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chain chain = db.Chain.Find(id);
            db.Chain.Remove(chain);
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
