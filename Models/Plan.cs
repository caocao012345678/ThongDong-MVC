using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThongDong_MVC.Models
{
    public class Plan
    {
        public TOUR Tour { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int QuantityAdult { get; set; }
        public int QuantityChild { get; set; }

        public Plan(TOUR tour, DateTime dateStart,DateTime dateEnd, int quantityAdult, int quantityChild)
        {
            Tour = tour;
            DateStart = dateStart;
            DateEnd = dateEnd;
            QuantityAdult = quantityAdult;
            QuantityChild = quantityChild;
        }
    }
}