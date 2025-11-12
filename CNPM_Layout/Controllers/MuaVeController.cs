using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CNPM_Layout.Models;
namespace CNPM_Layout.Controllers
{
    public class MuaVeController : Controller
    {
        public QLDATVEEntities ql = new QLDATVEEntities();
        public ActionResult Index()
        {
            ViewBag.dsPhongChieu = ql.PHONGCHIEUx.ToList();
            return View(ql.RAPs.ToList());
        }
        public ActionResult LocPhongChieu(string maRap)
        {
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x=>x.MARAP == maRap).ToList();
            List<RAP> rap = ql.RAPs.ToList();
            ViewBag.dsPhongChieu = pc;
            return View(pc);
        }
        public ActionResult LocTheoNgay(string maRap)
        {
            SUATCHIEU pc = ql.SUATCHIEUx.FirstOrDefault(x => x.MARAP == maRap);
            List<RAP> rap = ql.RAPs.Where(x => x.MARAP == pc.MARAP).ToList();
            return View(pc);
        }
        public ActionResult LocTheoLoaiManHinh(string maRap)
        {
            PHONGCHIEU pc = ql.PHONGCHIEUx.FirstOrDefault(x => x.MARAP == maRap);
            List<RAP> rap = ql.RAPs.Where(x => x.MARAP == pc.MARAP).ToList();
            return View(pc);
        }

    }
}