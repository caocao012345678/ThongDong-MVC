using ThongDong_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;
using System.Net;
using System.Diagnostics.Eventing.Reader;

namespace SecurityDemoMVC.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection field)
        {
            string UserName = Convert.ToString(field["UserName"]).ToLower();
            string UserPassword = GetMD5(Convert.ToString(field["UserPassword"]));
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "ThongDong");
            }
            using (ThongDongDBContext dBContext = new ThongDongDBContext())
            {
                bool IsValidUser = dBContext.ACCOUNTs.Any(user => user.USERNAME.ToLower() == UserName.ToLower()
                                                                                && user.PASSWORD == UserPassword);
                bool IsValidAdmin = dBContext.ACCOUNTs.Any(user => user.USERNAME.ToLower() == UserName.ToLower()
                                                                                && user.PASSWORD == UserPassword
                                                                                && user.ACCOUNT_TYPE == "ADMIN");
                if (IsValidAdmin)
                {
                    FormsAuthentication.SetAuthCookie(UserName, false);
                    return RedirectToAction("Index", "Maganement");
                }
                else
                {
                    if (IsValidUser)
                    {
                        FormsAuthentication.SetAuthCookie(UserName, false);
                        return RedirectToAction("Index", "ThongDong");
                    }
                }

                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "ThongDong");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection field)
        {
            ThongDongDBContext dBContext = new ThongDongDBContext();
            string User = Convert.ToString(field["Username"]).ToLower();
            string Email = Convert.ToString(field["Email"]);
            string Phone = Convert.ToString(field["Phone"]);
            string Pass = GetMD5(Convert.ToString(field["Password"]));
            string ConfirmPass = GetMD5(Convert.ToString(field["ConfirmPassword"]));

            if (Pass != ConfirmPass)
            {
                ModelState.AddModelError("", "Your password is not the same");
                return View();
            }
            bool exist = dBContext.ACCOUNTs.Any(user => user.USERNAME.ToLower() == User.ToLower());
            if (exist)
            {
                ModelState.AddModelError("", "Invalid Username");
                return View();
            }
            else
            {
                ACCOUNT A = new ACCOUNT()
                {
                    USERNAME = User,
                    EMAIL = Email,
                    PHONE = Phone,
                    PASSWORD = Pass,
                    ACCOUNT_TYPE = "USER"
                };
                dBContext.ACCOUNTs.Add(A);
                dBContext.SaveChanges();
                return RedirectToAction("Login");
            }
        }
        [Authorize]
        public ActionResult Info()
        {
            if (!User.Identity.IsAuthenticated) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            ThongDongDBContext db = new ThongDongDBContext();
            ACCOUNT user = db.ACCOUNTs.First(u => u.USERNAME == User.Identity.Name);
            return View(user);
        }
        [Authorize]
        public ActionResult InfoUpdate(FormCollection field, int id)
        {
            if (!User.Identity.IsAuthenticated) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            int Id = Convert.ToInt32(id);
            string user = Convert.ToString(field["Username"]).ToLower();
            string Email = Convert.ToString(field["Email"]);
            string Phone = Convert.ToString(field["Phone"]);
            ThongDongDBContext dBContext = new ThongDongDBContext();
            bool exist = dBContext.ACCOUNTs.Any(u => u.USERNAME.ToLower() == user.ToLower());
            if (exist)
            {
                ModelState.AddModelError("", "Invalid Username");
                return RedirectToAction("Info");
            }
            else
            {
                ACCOUNT acc = dBContext.ACCOUNTs.Find(Id);
                {
                    acc.USERNAME = user;
                    acc.EMAIL = Email;
                    acc.PHONE = Phone;
                    dBContext.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user, false);
                }
                return RedirectToAction("Info");
            }
        }
        [Authorize]
        public ActionResult UpdatePasswordView()
        {
            if (!User.Identity.IsAuthenticated) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            ThongDongDBContext db = new ThongDongDBContext();
            ACCOUNT user = db.ACCOUNTs.First(u => u.USERNAME == User.Identity.Name);
            return View(user);
        }
        [Authorize]
        public ActionResult UpdatePassword(FormCollection field, int id)
        {
            if (!User.Identity.IsAuthenticated) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            ThongDongDBContext dBContext = new ThongDongDBContext();
            int Id = Convert.ToInt32(id);
            ACCOUNT acc = dBContext.ACCOUNTs.Find(Id);
            string CurrentPass = GetMD5(Convert.ToString(field["CurrentPass"]));
            if (acc.PASSWORD != CurrentPass)
            {
                ModelState.AddModelError("", "Your password invalid");
                return View();
            }
            string Pass = GetMD5(Convert.ToString(field["Password"]));
            string ConfirmPass = GetMD5(Convert.ToString(field["ConfirmPassword"]));
            if (Pass != ConfirmPass)
            {
                ModelState.AddModelError("", "Your password is not the same");
                return View();
            }
            else
            {
                acc.PASSWORD = ConfirmPass;
                dBContext.SaveChanges();
                return RedirectToAction("Info");
            }
        }




        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}