using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using H1.Models;
using PagedList;
using PagedList.Mvc;


namespace H1.Controllers
{
    public class HomeController : Controller
    {
        GetData gd = new GetData();
        HerLin0030Entitieschain db=new HerLin0030Entitieschain();
        //int pageSize = 10;
        public ActionResult Index(string keyword)
        {
            //var store1=db.Store.Where(x => x.StoreIdentifyChineseName == storename).ToString().ToList();
            string sql = $"select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StoreIdentifyChineseName from Store where StorePay=0 AND StoreIdentifyChineseName LIKE '%{keyword}%'";
            //var store0 = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StoreIdentifyChineseName from Store where StorePay=0 ", CommandType.Text);
            var store0 = gd.querySql(sql, CommandType.Text);
            return View(store0);
        }
        public ActionResult IndexIn()
        {
            var store = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StorePay,StoreIdentifyChineseName from Store", CommandType.Text);
            return View(store);
        }
        //public ActionResult IndexIn(int page = 1)
        //{
        //    int currentPage = page < 1 ? 1 : page;
        //    var storeslist = db.Store.OrderBy(m => m.StoreIdentify).ToList();
        //    var result = storeslist.ToPagedList(currentPage, pageSize);
        //    var store = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StorePay,StoreIdentifyChineseName from Store", CommandType.Text);
        //    return View(result);
        //}
        [HttpGet]
        public ActionResult Login()
        {
            var store1 = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StoreIdentifyChineseName from Store", CommandType.Text);
            return View(store1);
        }

        [HttpPost]
        public ActionResult Login(string id,string password,int option)
        {
            //DB驗證id、password是否正確
            var result = db.Member.Where(x => x.MemberAccount == id && x.MemberPassword == password && x.MemberType == option).FirstOrDefault();
            //var result="select* from member where memberid = id and memberpwd = password";
            if (result != null)
            { 
                if (option == 1) { return RedirectToAction("第一個Action"); }//使用者會員，進來要看到所有店家資訊
                if (option == 2) { return RedirectToAction("第一個Action"); }//店家非會員，進來要看到自己的編輯畫面，沒有text跟pay
                if (option == 3) { return RedirectToAction("第一個Action"); }//店家會員，進來要看到自己的編輯畫面
                if (option == 4) { return RedirectToAction("IndexIn", "Home"); }
            }
            return View();
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}