using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThongDong_MVC.Models;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
using PagedList;
using System.Web.UI;

namespace ThongDong_MVC.Controllers
{
    public class ThongDongController : Controller
    {
        //nav
        public ActionResult Index() { return View(); }
        public ActionResult navAbout() { return View(); }
        public ActionResult navContact(FormCollection field)
        {
            ThongDongDBContext dBContext = new ThongDongDBContext();
            string Name = Convert.ToString(field["Name"]);
            string Messenger = Convert.ToString(field["Messenger"]);
            string Email = Convert.ToString(field["Email"]);
            int AccountId = -1;

            if (Name != null && Messenger != null && Email != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    ACCOUNT user = dBContext.ACCOUNTs.First(u => u.USERNAME == User.Identity.Name);
                    AccountId = user.ACCOUNT_ID;
                }
                var feedback = new ThongDong_MVC.Models.FEEDBACK()
                {
                    NAME = Name,
                    FEEDBACK_TEXT = Messenger,
                    EMAIL = Email,
                    FEEDBACK_DATE = DateTime.Now,
                };
                if (AccountId != -1) { feedback.ACCOUNT_ID = AccountId; }
                dBContext.FEEDBACKs.Add(feedback);
                dBContext.SaveChanges();
                return View();
            }
            return View();
        }
        public ActionResult navGuide(int? page)
        {
            ThongDongDBContext dBContext = new ThongDongDBContext();
            var pageNumber = page ?? 1;
            var pageSize = 4;
            var result = dBContext.GUIDEs.OrderBy(p => p.GUIDE_ID).ToPagedList(pageNumber, pageSize);
            return View(result);
        }

        public ActionResult navTour(FormCollection field)
        {
            int locationId = Convert.ToInt32(field["Location"]);
            ThongDongDBContext dBContext = new ThongDongDBContext();
            if (locationId == 0)
            {
                List<TOUR> ListTour = dBContext.TOURs.ToList();
                return View(ListTour);
            }
            else
            {
                List<TOUR> ListTour = dBContext.TOURs.Where(L => L.DESTINATION_ID == locationId).ToList();
                ViewBag.LocationID = locationId;
                return View(ListTour);
            }
        }

        public ActionResult guideDetails(int id)
        {
            ThongDongDBContext dbContext = new ThongDongDBContext();
            GUIDE GUIDE = dbContext.GUIDEs.FirstOrDefault(x => x.GUIDE_ID == id);
            return View(GUIDE);
        }
        public ActionResult tourDetails(int id)
        {
            ThongDongDBContext dbContext = new ThongDongDBContext();
            TOUR TOUR = dbContext.TOURs.FirstOrDefault(x => x.TOUR_ID == id);
            return View(TOUR);
        }





        //place
        public ActionResult placesDaLat() { return View(); }
        public ActionResult placesDaNang() { return View(); }
        public ActionResult placesNhaTrang() { return View(); }
        public ActionResult placesPhanThiet() { return View(); }
        public ActionResult placesPhuQuoc() { return View(); }
        public ActionResult placesPhuYen() { return View(); }
        public ActionResult placesQuyNhon() { return View(); }
        public ActionResult placesVungTau() { return View(); }
    }
}