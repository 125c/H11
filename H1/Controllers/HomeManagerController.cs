using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H1.Models;

namespace H1.Controllers
{
    public class HomeManagerController : Controller
    {
        private HerLin0030EntitiesStore db = new HerLin0030EntitiesStore();
        // GET: Homemanager
        //[LoginCheck]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(VMLogin vMLogin)
        //{
        //    var user=db.Employees.Where(m=>m.Account==vMLogin.Account&&m.Password==vMLogin.Password).FirstOrDefault();
        //    if (user == null) 
        //    {
        //        ViewBag.ErrMsg = "帳密錯誤!";
        //        return View(vMLogin);
        //    }
        //    Session["user"] = user;
        //    return RedirectToAction("Index");            
        //}
        //[LoginCheck]
        public ActionResult Logout() 
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Home");
        }
        //public ActionResult Index()
        //{
        //    return RedirectToAction("Login", "Home");
        //}
    }
}