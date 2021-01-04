using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Controllers
{
    public class TvController : Controller
    {
        TvBusiness _tvBusiness;
        YueBusiness _yueBusiness;
        public TvController()
        {
            _tvBusiness = new TvBusiness();
            _yueBusiness = new YueBusiness();
        }

        public IActionResult Index()
        {
            return View();
        }    
        public IActionResult BadRequest()
        {

            return View();
        }
        public IActionResult HrxHr(string yuegp,string Brk ,int begin,int end)
        {
            var BeginDay = DateTime.Now;
            var EndDay = DateTime.Today;
            
            ViewBag.SAudit = _yueBusiness.GetStationsInStations("SA01", yuegp);
            ViewBag.Bad = _yueBusiness.GetStationsInStations(Brk, yuegp);           

            ViewBag.GroupStation = _yueBusiness.GetYueGroupStation(yuegp);
            if (begin <= 0)
                begin = 1;

            ViewBag.DisplayBegin = begin - 1;
            if (end <= 0)
                end = 5;
            ViewBag.DisplayEnd = end-1;
            var result = _tvBusiness.HrxHr(yuegp, BeginDay, EndDay);
            if(result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("BadRequest");
            }
            
        }
     
    }
}
