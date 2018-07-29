using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MJSite.Models;

namespace MJSite.Controllers
{
    public class HomeController : Controller
    {
        private DbBase _DB;
        public HomeController(DbBase DB)
        {
            _DB = DB;
        }
        public async Task<IActionResult>  Index()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("user")))
            {
                //插入数据库
               await _DB.Users.AddAsync(new Model.UserModel {
                    UserID=Guid.NewGuid (),
                });
                 await _DB.SaveChangesAsync();
                

                //新登录的用户
                HttpContext.Session.SetString("user", "login");
            }
            ViewBag.UserCount= _DB.Users.AsQueryable().Count();


            //获取主页图片
           ViewBag.Images=_DB.Images.AsQueryable().OrderByDescending(m=>m.CreateTime).ToList();
          
          
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
