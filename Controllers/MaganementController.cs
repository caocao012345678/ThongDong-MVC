using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThongDong_MVC.Models;

namespace ThongDong_MVC.Controllers
{
    [Authorize]
    public class MaganementController : Controller
    {
        public ActionResult Index()
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            ThongDongDBContext dBContext = new ThongDongDBContext();
            ViewBag.Accounts = dBContext.ACCOUNTs.Count();
            ViewBag.Oders = dBContext.BOOKINGs.Count();
            ViewBag.People = dBContext.BOOKING_DETAIL_PEOPLE.Count();
            ViewBag.Tours = dBContext.TOURs.Count();
            ViewBag.Guides = dBContext.GUIDEs.Count();
            ViewBag.Feedback = dBContext.FEEDBACKs.Count();
            return View();
        }

        public ActionResult Tours()
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            return View();
        }
        public ActionResult Delete_Tour(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                var tourToDelete = db.TOURs.Find(ID);

                if (tourToDelete != null)
                {
                    db.TOURs.Remove(tourToDelete);
                    db.SaveChanges();
                    return RedirectToAction("Tours");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }
        public ActionResult Update(int id, FormCollection field)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            int priAdult = Convert.ToInt32(field["PriceAdult"]);
            int priChild = Convert.ToInt32(field["PriceChild"]);
            int endDate = Convert.ToInt32(field["EndDate"]);
            using (var db = new ThongDongDBContext())
            {
                var tourToUpdate = db.TOURs.Find(ID);

                if (tourToUpdate != null)
                {
                    tourToUpdate.PRICE_ADULT = priAdult;
                    tourToUpdate.PRICE_CHILD = priChild;
                    tourToUpdate.END_DATE = endDate;
                    db.SaveChanges();
                    return RedirectToAction("Tours");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }
        public ActionResult Tour_Detail(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                var tourToEdit = db.TOURs.Find(ID);
                if (tourToEdit != null)
                {
                    return View(tourToEdit);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        public ActionResult Account()
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            return View();
        }

        public ActionResult DeleteAccount(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int Id = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                var accountToDelete = db.ACCOUNTs.Find(Id);

                if (accountToDelete != null)
                {
                    db.ACCOUNTs.Remove(accountToDelete);
                    db.SaveChanges();
                    return RedirectToAction("Account");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }
        public ActionResult Account_Detail(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);

            using (var db = new ThongDongDBContext())
            {
                var AccountToEdit = db.ACCOUNTs.Find(ID);
                if (AccountToEdit != null)
                {
                    return View(AccountToEdit);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

        }

        public ActionResult Account_Update(int id, FormCollection field)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            int role = Convert.ToInt32(field["role"]);
            if (role != 0 && role != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (role == 0)
                {
                    string roleName = "USER";
                    using (var db = new ThongDongDBContext())
                    {
                        var Acc = db.ACCOUNTs.Find(ID);
                        Acc.ACCOUNT_TYPE = roleName;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Account");
                }
                else if (role == 1)
                {
                    string roleName = "ADMIN";
                    using (var db = new ThongDongDBContext())
                    {
                        var Acc = db.ACCOUNTs.Find(ID);
                        Acc.ACCOUNT_TYPE = roleName;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Account");
                }
                else return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult OderProcess()
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            return View();
        }
        public ActionResult OderProcessDetail(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                BOOKING_DETAIL bookingDetail = db.BOOKING_DETAIL.Find(ID);
                if (bookingDetail != null)
                {
                    List<BOOKING_DETAIL_PEOPLE> bookingDetailPeople =
                        db.BOOKING_DETAIL_PEOPLE.Where(p => p.BOOKING_ID == bookingDetail.BOOKING_ID).ToList();
                    ViewBag.bookingDetail = bookingDetail;
                    ViewBag.TourName = bookingDetail.TOUR.NAME;
                    ViewBag.Status = bookingDetail.BOOKING.STATUS;
                    return View(bookingDetailPeople);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        public ActionResult DeleteOder(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int ID = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                var oderToDelete = db.BOOKINGs.Find(ID);

                if (oderToDelete != null)
                {
                    db.BOOKINGs.Remove(oderToDelete);
                    db.BOOKING_DETAIL.Remove(db.BOOKING_DETAIL.Find(ID));
                    List<BOOKING_DETAIL_PEOPLE> bookingDetailPeople = db.BOOKING_DETAIL_PEOPLE.Where(p => p.BOOKING_ID == ID).ToList();
                    db.BOOKING_DETAIL_PEOPLE.RemoveRange(bookingDetailPeople);
                    db.SaveChanges();
                    return RedirectToAction("OderProcess");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }

        public ActionResult Feedback()
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            return View();
        }

        public ActionResult DeleteFeedback(int id)
        {
            if (NotAccess()) { return RedirectToAction("Login", "Accounts"); }
            int Id = Convert.ToInt32(id);
            using (var db = new ThongDongDBContext())
            {
                var FeedbackToDelete = db.FEEDBACKs.Find(Id);

                if (FeedbackToDelete != null)
                {
                    db.FEEDBACKs.Remove(FeedbackToDelete);
                    db.SaveChanges();
                    return RedirectToAction("Feedback");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
        }












        public Boolean NotAccess()
        {
            ThongDongDBContext db = new ThongDongDBContext();
            ACCOUNT user = db.ACCOUNTs.First(u => u.USERNAME == User.Identity.Name);
            if (user.ACCOUNT_TYPE == "ADMIN") { return false; }
            else { return true; }
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