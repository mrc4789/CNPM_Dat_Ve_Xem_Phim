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
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            return View(ql.RAPs.ToList());
        }
        public ActionResult _NgayChieu()
        {

            return View(ql.SUATCHIEUx.ToList());
        }
        //Tìm kiếm rạp và suất chiếu theo ngày chiếu
        public ActionResult LocTheoNgay(string maRap, DateTime ngayChieu)
        {
            List<SUATCHIEU> ngay = ql.SUATCHIEUx.Where(x => x.MARAP == maRap).ToList().Where(x => x.NGAYCHIEU.HasValue && x.NGAYCHIEU.Value.Date == ngayChieu.Date).ToList();
            //List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MALOAI == maLoai).ToList();
            List<RAP> rap = ql.RAPs.Where(x => x.MARAP == maRap).ToList();
            //ViewBag.dsPhongChieu = pc;
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            ViewBag.DSNgayChieu = ngay;
            return View("Index", ngay);
        }
        //Tìm kiếm rạp và suất chiếu theo địa chỉ
        public ActionResult LocTheoDiaChi(string marap)
        {
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MARAP == marap).ToList();
            List<RAP> diaChiRap = ql.RAPs.Where(x => x.MARAP == marap).ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            ViewBag.rapDuocChon = marap;
            return View("Index", diaChiRap);
        }
        public ActionResult ChonGhe()
        {
            return View();
        }
        public ActionResult _LoaiManHinh()
        {
            return View(ql.LOAIMANHINHs.ToList());
        }
        //Tìm kiếm rạp và suất chiếu theo loại màn hình
        public ActionResult LocTheoLoaiManHinh(string maLoai, string marap)
        {
            LOAIMANHINH loaiMH = ql.LOAIMANHINHs.FirstOrDefault(x => x.MALOAI == maLoai);
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MALOAI == maLoai && x.MALOAI == loaiMH.MALOAI).ToList();
            List<RAP> rap = ql.RAPs.ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            ViewBag.rapDuocChon = marap;
            return View("Index", rap);
        }
        public ActionResult _SuatChieu(string marap)
        {
            List<SUATCHIEU> sc = ql.SUATCHIEUx.Where(x => x.MARAP == marap).OrderBy(x => x.NGAYCHIEU).ThenBy(x => x.GIOCHIEU).ToList();
            return View(sc);
        }
     
    }
}