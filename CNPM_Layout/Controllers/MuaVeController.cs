using CNPM_Layout.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace CNPM_Layout.Controllers
{
    public class MuaVeController : Controller
    {
        public QLDATVEEntities ql = new QLDATVEEntities();
        public ActionResult Index(string marap)
        {
            List<RAP> rap = ql.RAPs.ToList();
            if (string.IsNullOrEmpty(marap) && rap.Any())
                marap = rap.First().MARAP;
            ViewBag.rapDuocChon = marap;
            ViewBag.dsPhongChieu = ql.PHONGCHIEUx.ToList();
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            return View(rap);
        }

        public ActionResult _NgayChieu(string marap)
        {
            ViewBag.rapDuocChon = marap;
            return PartialView();
        }

        //Tìm kiếm rạp và suất chiếu theo ngày chiếu
        public ActionResult LocTheoNgay(string marap, DateTime ngayChieu)
        {
            List<SUATCHIEU> ngay = ql.SUATCHIEUx.Where(x => x.MARAP == marap).ToList().Where(x => x.NGAYCHIEU.HasValue && x.NGAYCHIEU.Value.Date == ngayChieu.Date).ToList();
            List<PHONGCHIEU> pc = ql.PHONGCHIEUx.Where(x => x.MARAP == marap).ToList();
            List<RAP> rap = ql.RAPs.Where(x => x.MARAP == marap).ToList();
            ViewBag.dsPhongChieu = pc;
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            ViewBag.DSNgayChieu = ngay;
            ViewBag.rapDuocChon = marap;
            return View("Index", rap);
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
        public ActionResult _LoaiManHinh(string marap)
        {
            ViewBag.rapDuocChon = marap;
            return View(ql.LOAIMANHINHs.ToList());
        }

        //Tìm kiếm rạp và suất chiếu theo loại màn hình
        public ActionResult LocTheoLoaiManHinh(string maLoai, string marap)
        {
            LOAIMANHINH loaiMH = ql.LOAIMANHINHs.FirstOrDefault(x => x.MALOAI == maLoai);
            List<RAP> rap = ql.RAPs.ToList();
            List<PHONGCHIEU> pc = new List<PHONGCHIEU>();
            if (!string.IsNullOrEmpty(marap))
            {
                 pc = ql.PHONGCHIEUx.Where(x => x.MALOAI == maLoai && x.MARAP == marap).ToList();
            }
            ViewBag.dsPhongChieu = pc;
            ViewBag.ManHinh = ql.LOAIMANHINHs.ToList();
            ViewBag.LoaiMHChon = maLoai;
            ViewBag.TenLoaiMH = loaiMH.MANHINH;
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