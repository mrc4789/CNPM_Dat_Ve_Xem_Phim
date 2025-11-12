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
        public ActionResult LocTheoNgay(string maRap, DateTime ngayChieu)
        {
            List<SUATCHIEU> ngay = ql.SUATCHIEUx.Where(x => x.MARAP == maRap && x.NGAYCHIEU.Value.Date == ngayChieu.Date).ToList();
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MARAP == maRap).ToList();
            List<RAP> rap = ql.RAPs.Where(x => x.MARAP == maRap).ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.DSNgayChieu = ngay;
            return View("Index", rap);
        }
        //Tìm kiếm rạp và suất chiếu theo địa chỉ
        public ActionResult LocTheoDiaChi(string marap)
        {
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MARAP == marap).ToList();
            List<RAP> diaChiRap = ql.RAPs.Where(x => x.MARAP == marap).ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.rapDuocChon = marap;
            return View("Index", diaChiRap);
        }
        //Tìm kiếm rạp và suất chiếu theo loại màn hình
        public ActionResult LocTheoLoaiManHinh(string maRap, string loai)
        {

            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MARAP == maRap && x.LOAIMANHINH == loai).ToList();
            List<RAP> loaiMH = ql.RAPs.Where(x => x.MARAP == maRap).ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.LoaiMHChon = loai;
            return View("Index", loaiMH);
        }

    }
}