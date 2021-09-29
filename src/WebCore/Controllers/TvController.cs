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
        public IActionResult Defects()
        {
            var result = _tvBusiness.DefectsDecide();

            if (result != null)
                return View(result);
            else
                return RedirectToAction("BadRequest");
        }
        public IActionResult HrxHr(string yuegp,string Brk,string Rw ,int begin,int end, int Day)
        {            
            var BeginDay = DateTime.Now;
            var EndDay = DateTime.Today;
            if (Day > 0)
            {
                BeginDay = DateTime.Now.AddDays(-Day);
                EndDay = BeginDay.AddDays(+1);
            }
            else
            {
                BeginDay = DateTime.Now;
                EndDay = BeginDay.AddDays(+1);
            }
            ViewBag.Day = Day;

            ViewBag.Defects = _yueBusiness.YueHrxhrDecide(yuegp,BeginDay,EndDay);            
            ViewBag.SAudit = _yueBusiness.GetStationsInStations("S_07", yuegp);
            ViewBag.Rework = _yueBusiness.GetStationsInStations(Rw, yuegp);
            ViewBag.Bad = _yueBusiness.GetStationsInStations(Brk, yuegp);
            

            ViewBag.GroupStation = _yueBusiness.GetYueGroupStation(yuegp);
            if (begin <= 0)
                begin = 1;

            ViewBag.DisplayBegin = begin - 1;
            if (end <= 0)
                end = 5;
            ViewBag.DisplayEnd = end-1;
            var result = _tvBusiness.HrxHr(yuegp, BeginDay);            
            if(result != null)
                return View(result);
            else
                return RedirectToAction("BadRequest");
            
        }
        public IActionResult HrxHrByAr(string yuegp, int begin, int end)
        {
            var BeginDay = DateTime.Now;
            var EndDay = DateTime.Now;

            ViewBag.GroupStation = _yueBusiness.GetYueGroupStation(yuegp);         

            if (begin <= 0)
                begin = 1;

            ViewBag.DisplayBegin = begin - 1;
            if (end <= 0)
                end = 5;
            ViewBag.DisplayEnd = end - 1;
            var result = _tvBusiness.HRXHRAR(yuegp);
            if (result != null)
                return View(result);
            else
                return RedirectToAction("BadRequest");
           
        }
        public IActionResult Dashboard()
        {
            ViewBag.Brk = _yueBusiness.BrkOpti();          
            var result = _yueBusiness.DashBoard();        
            if (result != null)
                return View(result);
            else
                return RedirectToAction("BadRequest");
            
        }
        public IActionResult BadRequest()
        {            
            return View();
        }
        public IActionResult ValidationMachines()
        {
          
            return View();
        }

    }
}
