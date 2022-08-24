using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H1.Models;
using System.Data.SqlClient;


namespace H1.Controllers
{
    public class LoginController : Controller
    {
        GetData gd = new GetData();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string name)
        {
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id", id),
                new SqlParameter("name",name)
            };
            string sqlm = "select * from Member where MemberAccount=@id and MemberPassword=@name";
            var dt = gd.querySql(sqlm, System.Data.CommandType.Text,list);

            if(dt.Rows.Count==0)
            {
                ViewBag.Msg = "帳號或密碼有誤!";
                return View();
            }
            return RedirectToAction("IndexIn", "Home"); 
        }
    }
}