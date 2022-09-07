using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using H1.Models;
using System.Data.Entity;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace H1.Controllers
{
    public class upPhotoController : Controller
    {
        HerLin0030Entitieschain db = new HerLin0030Entitieschain();
        // GET: upPhoto
        //public ActionResult upPhoto(Store s, StorePicture sp, HttpPostedFileBase photo)
        //{
        //    if (photo == null)
        //    {
        //        ViewBag.ErrMessage = "請上傳商品照片";
        //        return View(sp);
        //    }
        //    if (db.StorePicture.Find(sp.StorePictureNumberTime) != null)
        //    {
        //        ViewBag.ErrMessage2 = "照片時間重複";
        //        return View(sp);
        //    }
        //    sp.StorePictureNumberPicture = new byte[photo.ContentLength];
        //    photo.InputStream.Read(sp.StorePictureNumberPicture, 0, photo.ContentLength);
        //    sp.StorePictureNumberTime = DateTime.Today;
        //    s.StorePay = false;
        //    ModelState.Remove("StorePictureNumberPicture");
        //    if (ModelState.IsValid)
        //    {
        //        db.StorePicture.Add(sp);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");
        //}
        //public FileContentResult GetImage(string id)
        //{
        //    var photo = db.StorePicture.Find(id);
        //    if (photo == null)
        //        return null;
        //    return File(photo.StorePictureNumberPicture, photo.ImageMimeType);
        //}

        public ActionResult UploadImage(HttpPostedFileBase File) 
        {
            if (File != null && File.ContentLength > 0)
            {
                //存到資料夾
                var FileName = Path.GetFileName(File.FileName);
            var FilePath = Path.Combine(Server.MapPath("~/Images/"), FileName);
            File.SaveAs(FilePath);


            //轉成byte 方法一 直接轉
            byte[] FileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                File.InputStream.CopyTo(ms);
                FileBytes = ms.GetBuffer();
            }
            //存進資料庫再取出
            InsertPhoto(FileBytes);
            FileBytes = SelectPhoto();

            TempData["Data"] = FileBytes;
        }
            return View();
    }
        public void InsertPhoto(byte[] photo)
        {
            string str = "Insert into StorePicture(Image) values(@StorePictureNumberPicture)";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.Parameters.AddWithValue(@"StorePictureNumberPicture", photo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public byte[] SelectPhoto()
        {
            byte[] photo;
            string str = "select top 1 StorePictureNumberPicture from StorePicture ";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    photo = (byte[])reader["StorePictureNumberPicture"];
                    con.Close();
                    return photo;
                }
                else
                    return null;
            }
        }
        public ActionResult Index()
        {
            return View(db.StorePicture.ToList());
        }
        
        public FileContentResult GetImage(string id)
        {
            var photo = db.StorePicture.Find(id);
            if (photo == null)
                return null;
            return File(photo.StorePictureNumberPicture, photo.ImageMimeType);
        }
        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePicture products = db.StorePicture.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return PartialView(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Products/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
       
        public ActionResult Create(StorePicture products, HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                ViewBag.ErrMessage = "請上傳商品照片";
                return View(products);
            }
            if (db.StorePicture.Find(products.StorePictureNumberTime) != null)
            {
                ViewBag.ErrMessage2 = "商品編號重複";
                return View(products);
            }
            products.StorePictureNumberPicture = new byte[photo.ContentLength];
            photo.InputStream.Read(products.StorePictureNumberPicture, 0, photo.ContentLength);
            products.StorePictureNumberTime = DateTime.Today;
            //products.Discontinued = false;
            ModelState.Remove("PhotoFile");
            if (ModelState.IsValid)
            {
                db.StorePicture.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePicture products = db.StorePicture.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(StorePicture products, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                //products.ImageMimeType = photo.ContentType;
                products.StorePictureNumberPicture = new byte[photo.ContentLength];
                photo.InputStream.Read(products.StorePictureNumberPicture, 0, photo.ContentLength);
            }
            ModelState.Remove("StorePictureNumberPicture");
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult ProductStatusChange(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePicture products = db.StorePicture.Find(id);
            db.Entry(products).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePicture products = db.StorePicture.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(string id)
        {
            StorePicture products = db.StorePicture.Find(id);
            db.StorePicture.Remove(products);
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