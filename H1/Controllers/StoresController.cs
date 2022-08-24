using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using H1.Models;

namespace H1.Controllers
{
    public class StoresController : Controller
    {
        private HerLin0030EntitiesStore db = new HerLin0030EntitiesStore();
        SetData sd = new SetData();
        private string ChainIdentify, ChainName;
        //GET: Stores
        public ActionResult Index()
        {
            //var store = db.Store.Include(s => s.Chain);
            //return View(store.ToList());
            return View(db.Store.ToList());
        }

        //// GET: Stores/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Store store = db.Store.Find(id);
        //    if (store == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(store);
        //}
        public ActionResult Create()
        {
            List<Chain> chains= db.Chain.ToList();
            List<SelectListItem> ChainListItem = new List<SelectListItem>();
            foreach (var x in chains)  
            {    
                ChainListItem.Add(new SelectListItem {Value = x.ChainIdentify, Text = x.ChainName });
            }
            ViewBag.ChainList = ChainListItem;


            
            ViewData["select ChainIdentify from Chain"] = "select ChainName from Chain";
            ViewBag.SelectListItem = ChainListItem;
            return View();
        }
        // GET: Stores/Create
        [HttpPost]
        public ActionResult Create(String StoreIdentify, String StoreAddress,decimal StoreLocationX,decimal StoreLocationY, String StoreTelephone, bool StorePay, String StoreText, String StoreIdentifyChain, String StoreIdentifyChineseName)
        {
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("@StoreIdentify", StoreIdentify),
                new SqlParameter("@StoreAddress",StoreAddress),
                new SqlParameter("@StoreLocationX",StoreLocationX),
                new SqlParameter("@StoreLocationY",StoreLocationY),
                new SqlParameter("@StoreTelephone",StoreTelephone),
                new SqlParameter("@StorePay",StorePay),
                new SqlParameter("@StoreText",StoreText),
                new SqlParameter("@StoreIdentifyChain",StoreIdentifyChain),
                new SqlParameter("@StoreIdentifyChineseName",StoreIdentifyChineseName)
            };
            string sql = "insert into Store (StoreIdentify,StoreAddress,StoreLocationX,StoreLocationY,StoreTelephone,StorePay,StoreText,StoreIdentifyChain,StoreIdentifyChineseName) values(@StoreIdentify,@StoreAddress,@StoreLocationX,@StoreLocationY,@StoreTelephone,@StorePay,@StoreText,@StoreIdentifyChain,@StoreIdentifyChineseName)";
            sd.executeSql(sql, list);
            return View();
        }


        // POST: Stores/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "StoreIdentify,StoreAddress,StoreLocationX,StoreLocationY,StoreTelephone,StorePay,StoreText,StoreIdentifyChain,StoreIdentifyChineseName")] Store store)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Store.Add(store);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.StoreIdentifyChain = new SelectList(db.Chain, "ChainIdentify", "ChainName", store.StoreIdentifyChain);
        //    return View(store);
        //}

        //// GET: Stores/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Store store = db.Store.Find(id);
        //    if (store == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.StoreIdentifyChain = new SelectList(db.Chain, "ChainIdentify", "ChainName", store.StoreIdentifyChain);
        //    return View(store);
        //}

        //// POST: Stores/Edit/5
        //// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "StoreIdentify,StoreAddress,StoreLocationX,StoreLocationY,StoreLocation,StoreTelephone,StorePay,StoreText,StoreIdentifyChain,StoreIdentifyChineseName")] Store store)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(store).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.StoreIdentifyChain = new SelectList(db.Chain, "ChainIdentify", "ChainName", store.StoreIdentifyChain);
        //    return View(store);
        //}

        //// GET: Stores/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Store store = db.Store.Find(id);
        //    if (store == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(store);
        //}

        //// POST: Stores/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Store store = db.Store.Find(id);
        //    db.Store.Remove(store);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
