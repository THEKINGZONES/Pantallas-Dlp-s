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
        public IActionResult brkDlps()
        {
            return View(_tvBusiness.brkDlps());
        }
        public IActionResult ListStations()
        {
            return View(_tvBusiness.ListStations());
        }
        public IActionResult HrxHr(string yuegp)
        {
            var BeginDay = DateTime.Now;
            var EndDay = DateTime.Today;
            ViewBag.GroupStation = _yueBusiness.GetYueGroupStation(yuegp);

            return View(_tvBusiness.HrxHr(yuegp, BeginDay,EndDay));
        }
     
    }
}
