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
        public TvController()
        {
            _tvBusiness = new TvBusiness();
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
        public IActionResult HrxHr()
        {
            var BeginDay = new DateTime(2020, 11, 15);
            var EndDay = new DateTime(2020, 11, 16);

            return View(_tvBusiness.HrxHr(8015,BeginDay,EndDay));
        }

    }
}
