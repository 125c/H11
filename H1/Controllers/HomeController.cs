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

namespace H1.Controllers
{
    public class HomeController : Controller
    {
        GetData gd = new GetData();
        HerLin0030Entitieschain db=new HerLin0030Entitieschain();
        public ActionResult Index()
        {
            var store0 = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StoreIdentifyChineseName from Store where StorePay=0 ", CommandType.Text);
            return View(store0);
        }
        public ActionResult IndexIn()
        {
            var store = gd.querySql("select StoreIdentify,StoreAddress,StoreTelephone,StoreText,StorePay,StoreIdentifyChineseName from Store", CommandType.Text);
            return View(store);
        }
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