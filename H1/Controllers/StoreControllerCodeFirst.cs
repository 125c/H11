using H1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace H1.Controllers
{
    public class StoreControllerCodeFirst : Controller
    {
        private HerLin0030EntitiesStore db = new HerLin0030EntitiesStore();

        public object Store { get; private set; }

        // GET: Store
        public ActionResult Index(int page = 1)
        {
            var store = db.Store.ToList();
            int pagesize = 5;
            var PagedList = Store.ToPagedList(page, pagesize);
            return View(PagedList);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store stores = db.Store.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return PartialView(stores);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoreIdentify,StoreAddress,StoreLocationX,StoreLocationY,StoreLocation,StoreTelephone,StorePay,StoreText,StoreIdentifyChain,StoreIdentifyChineseName")] Store stores)
        {
            if (ModelState.IsValid)
            {
                db.Store.Add(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store stores = db.Store.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Store stores)
        {
            string sql = "update Store set StoreIdentify=@StoreIdentify,StoreAddress=@StoreAddress,StoreLocationX=@StoreLocationX,StoreLocationY=@StoreLocationY,StoreLocation=@StoreLocation,StoreTelephone=@StoreTelephone,StorePay=@StorePay,StoreText=@StoreText,StoreIdentifyChain=@StoreIdentifyChain,StoreIdentifyChineseName=@StoreIdentifyChineseName where StoreIdentify=@StoreIdentify";


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["aaa"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StoreIdentify", stores.StoreIdentify);
            cmd.Parameters.AddWithValue("@StoreAddress", stores.StoreAddress);
            cmd.Parameters.AddWithValue("@StoreLocationX", stores.StoreLocationX);
            cmd.Parameters.AddWithValue("@StoreLocationY", stores.StoreLocationY);
            cmd.Parameters.AddWithValue("@StoreLocation", stores.StoreLocation);
            cmd.Parameters.AddWithValue("@StoreTelephone", stores.StoreTelephone);
            cmd.Parameters.AddWithValue("@StorePay", stores.StorePay);
            cmd.Parameters.AddWithValue("@StoreText", stores.StoreText);
            cmd.Parameters.AddWithValue("@StoreIdentifyChain", stores.StoreIdentifyChain);
            cmd.Parameters.AddWithValue("@StoreIdentifyChineseName", stores.StoreIdentifyChineseName);
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            cmd.ExecuteNonQuery();

            conn.Close();
            return View(stores);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store stores = db.Store.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store stores = db.Store.Find(id);
            db.Store.Remove(stores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
