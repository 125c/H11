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

        //GET: Stores
        public ActionResult Index()
        {
            //var store = db.Store.Include(s => s.Chain);
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
        public ActionResult Create(String StoreIdentify, String StoreAddress,Decimal StoreLocationX,Decimal StoreLocationY, 
                                   String StoreTelephone, bool StorePay, String StoreText, String StoreIdentifyChain, 
                                   String StoreIdentifyChineseName,int PictureTypeNumber,
                                   HttpPostedFileBase StorePictureNumberPicture)
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

            List<SqlParameter> list2 = new List<SqlParameter>
            {
                new SqlParameter("@StorePictureNumberSID",StoreIdentify),
                new SqlParameter("@StorePictureNumberType",PictureTypeNumber),
                new SqlParameter("@StorePictureNumberPicture",StorePictureNumberPicture)
            };
            string sqlstore = "insert into Store (StoreIdentify,StoreAddress,StoreLocationX,StoreLocationY,StoreTelephone,StorePay,StoreText,StoreIdentifyChain,StoreIdentifyChineseName) values(@StoreIdentify,@StoreAddress,@StoreLocationX,@StoreLocationY,@StoreTelephone,@StorePay,@StoreText,@StoreIdentifyChain,@StoreIdentifyChineseName)";
            //↑店家表
            string spType = "insert into StorePicture  VALUES (getdate(),@StorePictureNumberSID,@StorePictureNumberType,@StorePictureNumberPicture)";
            //↑店家照片
            //string sql = "insert all into store s (s.StoreIdentify,s.StoreAddress,s.StoreLocationX,s.StoreLocationY,s.StoreTelephone,s.StorePay,s.StoreText,s.StoreIdentifyChain,s.StoreIdentifyChineseName) VALUES (@StoreIdentify,@StoreAddress,@StoreLocationX,@StoreLocationY,@StoreTelephone,@StorePay,@StoreText,@StoreIdentifyChain,@StoreIdentifyChineseName) into StorePicture p (p.StorePictureNumberTime,p.StorePictureNumberPicture) VALUES (@getdate(),@StorePictureNumberPicture)";
            //上面要加上insert圖片....先在ssms寫好insert兩個table
            sd.executeSql(sqlstore, list);
            sd.executeSql(spType, list2);
            return RedirectToAction("Index", "Stores");
        }
        public ActionResult upPhoto(Store s,StorePicture sp,HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                ViewBag.ErrMessage = "請上傳商品照片";
                return View(sp);
            }
            if (db.StorePicture.Find(sp.StorePictureNumberTime) != null)
            {
                ViewBag.ErrMessage2 = "照片時間重複";
                return View(sp);
            }
            sp.StorePictureNumberPicture = new byte[photo.ContentLength];
            photo.InputStream.Read(sp.StorePictureNumberPicture, 0, photo.ContentLength);
            sp.StorePictureNumberTime = DateTime.Today;
            s.StorePay = false;
            ModelState.Remove("StorePictureNumberPicture");
            if (ModelState.IsValid)
            {
                db.StorePicture.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
