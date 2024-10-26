using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThongDong_MVC.Models;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace ThongDong_MVC.Controllers
{
    public class PlanController : Controller
    {
        private ThongDongDBContext dBContext = new ThongDongDBContext();
        private string strPlan = "Plan";
        public List<string> NameAdult = new List<string>();
        public List<string> Email = new List<string>();
        public List<string> Phone = new List<string>();
        public List<string> NameChild = new List<string>();

        public ActionResult Gio_Hang() { return View(); }

        public ActionResult OrderNow(int Id, FormCollection field)
        {
            int id = Convert.ToInt32(Id);
            int quantityAdult = Convert.ToInt32(field["QuantityAdult"]);
            int quantityChild = Convert.ToInt32(field["QuantityChild"]);
            DateTime dateStart = Convert.ToDateTime(field["DateStart"]);
            DateTime dateEnd;

            if (quantityAdult == 0) { quantityAdult++; }
            if (dateStart == default)
            {
                dateStart = dateEnd = DateTime.Today;
            }

            int day = Convert.ToInt32(dBContext.TOURs.Where(ID => ID.TOUR_ID == id).Select(a => a.END_DATE).FirstOrDefault());
            dateEnd = dateStart.AddDays(day);

            if (Session[strPlan] == null)
            {
                List<Plan> ListPlan = new List<Plan>
                {
                    new Plan(dBContext.TOURs.Find(Id),dateStart,dateEnd,quantityAdult,quantityChild)
                };
                Session[strPlan] = ListPlan;
            }
            else
            {
                List<Plan> ListPlan = (List<Plan>)Session[strPlan];
                int check = IsExistingCheck(Id);
                if (check == -1)
                    ListPlan.Add(new Plan(dBContext.TOURs.Find(Id), dateStart, dateEnd, quantityAdult, quantityChild));
                else
                {
                    ListPlan[check].QuantityAdult = ListPlan[check].QuantityAdult + quantityAdult;
                    ListPlan[check].QuantityChild = ListPlan[check].QuantityChild + quantityChild;
                    Session[strPlan] = ListPlan;
                }
            }
            return RedirectToAction("navTour", "ThongDong");
        }
        private int IsExistingCheck(int? Id)
        {
            List<Plan> ListPlan = (List<Plan>)Session[strPlan];
            for (int i = 0; i < ListPlan.Count; i++)

            {
                if (ListPlan[i].Tour.TOUR_ID == Id)
                    return i;

            }
            return -1;
        }
        public ActionResult RemoveItem(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(Id);
            List<Plan> ListPlan = (List<Plan>)Session[strPlan];
            ListPlan.RemoveAt(check);
            if (ListPlan.Count == 0)
            {
                Session[strPlan] = null;
            }
            else
            {
                Session[strPlan] = ListPlan;
            }
            return RedirectToAction("Plan");
        }

        public void Remove1Item(int Id)
        {
            int check = IsExistingCheck(Id);
            List<Plan> ListPlan = (List<Plan>)Session[strPlan];
            ListPlan.RemoveAt(check);
            if (ListPlan.Count == 0)
            {
                Session[strPlan] = null;
            }
            else
            {
                Session[strPlan] = ListPlan;
            }
        }

        [HttpPost]
        public ActionResult UpdatePlan(FormCollection field)
        {
            string[] quantityAdult = field.GetValues("quantityAdult");
            string[] quantityChild = field.GetValues("quantityChild");
            string[] dateStart = field.GetValues("DateStart");
            List<Plan> ListPlan = (List<Plan>)Session[strPlan];
            for (int i = 0; i < ListPlan.Count; i++)
            {
                ListPlan[i].QuantityAdult = Convert.ToInt32(quantityAdult[i]);
                ListPlan[i].QuantityChild = Convert.ToInt32(quantityChild[i]);
                ListPlan[i].DateStart = Convert.ToDateTime(dateStart[i]);
                int day = Convert.ToInt32(ListPlan[i].Tour.END_DATE);
                ListPlan[i].DateEnd = ListPlan[i].DateStart.AddDays(day);
            }
            Session[strPlan] = ListPlan;
            return RedirectToAction("Plan");
        }
        public ActionResult ClearPlan()
        {
            Session[strPlan] = null;
            return RedirectToAction("Plan");
        }
        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult CheckOutInfomation(int id, DateTime dateStart, DateTime dateEnd, int quantityAdult, int quantityChild)
        {
            ViewBag.id = Convert.ToInt32(id);
            ViewBag.dateStart = Convert.ToDateTime(dateStart);
            ViewBag.dateEnd = Convert.ToDateTime(dateEnd);
            ViewBag.quantityAdult = Convert.ToInt32(quantityAdult);
            ViewBag.quantityChild = Convert.ToInt32(quantityChild);
            return View();
        }


        [HttpPost]
        public ActionResult ProcessOrder(int Id, int QA, int QC, DateTime DateStart, DateTime DateEnd, FormCollection field)
        {
            TOUR Tour = (TOUR)dBContext.TOURs.Where(ID => ID.TOUR_ID == Id).FirstOrDefault();
            int id = Convert.ToInt32(Id);
            int quantityAdult = Convert.ToInt32(QA);
            int quantityChild = Convert.ToInt32(QC);
            DateTime dateStart = Convert.ToDateTime(DateStart);
            DateTime dateEnd = Convert.ToDateTime(DateEnd);

            var booking = new ThongDong_MVC.Models.BOOKING()
            {
                BOOKING_ID = dBContext.BOOKINGs.Count(),
                ORDER_DATE = DateTime.Now,
                STATUS = "Processing",
                QUANTITY_ADULT = quantityAdult,
                QUANTITY_CHILD = quantityChild
            };
            dBContext.BOOKINGs.Add(booking);
            dBContext.SaveChanges();

            for (int i = 0; i < quantityAdult; i++)
            {
                string NameAdult = (field["NameAdult" + i]).ToString();
                string Phone = (field["Phone" + i]).ToString();
                string Email = (field["Email" + i]).ToString();

                var bookingDetailPeople = new ThongDong_MVC.Models.BOOKING_DETAIL_PEOPLE()
                {
                    BOOKING_DETAIL_PEOPLE_ID = dBContext.BOOKING_DETAIL_PEOPLE.Count(),
                    BOOKING_ID = booking.BOOKING_ID,
                    NAME = NameAdult,
                    PHONE = Phone,
                    EMAIL = Email
                };
                dBContext.BOOKING_DETAIL_PEOPLE.Add(bookingDetailPeople);
                dBContext.SaveChanges();
            }

            if (QC != 0)
            {
                for (int qc = 0; qc < QC; qc++)
                {
                    string NameChild = (field["NameChild" + qc]).ToString();
                    var bookingDetailPeople = new ThongDong_MVC.Models.BOOKING_DETAIL_PEOPLE()
                    {
                        BOOKING_DETAIL_PEOPLE_ID = dBContext.BOOKING_DETAIL_PEOPLE.Count(),
                        BOOKING_ID = booking.BOOKING_ID,
                        NAME = NameChild
                    };
                    dBContext.BOOKING_DETAIL_PEOPLE.Add(bookingDetailPeople);
                    dBContext.SaveChanges();
                }
            }            
            BOOKING_DETAIL bookingDetail = new BOOKING_DETAIL()
            {
                BOOKING_DETAIL_ID = dBContext.BOOKING_DETAIL.Count(),
                BOOKING_ID = booking.BOOKING_ID,
                TOUR_ID = Id,
                BOOKING_PEOPLE = booking.BOOKING_ID,
                QUANTITY_ADULT = quantityAdult,
                QUANTITY_CHILD = quantityChild,
                PRICE_ADULT = Tour.PRICE_ADULT,
                PRICE_CHILD = Tour.PRICE_CHILD,
                START_DATE = dateStart,
                END_DATE = dateEnd,
                OTHER_REQUIREMENTS = field["Other"].ToString(),
            };
            dBContext.BOOKING_DETAIL.Add(bookingDetail);
            dBContext.SaveChanges();
            Remove1Item(id);
            return RedirectToAction("OrderSuccess", "Plan");
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}