using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.UpdatePerson
{
    public class HoSos
    {
        public bool success { get; set; }
        public List<HoSo> hoSos { get; set; }
    }
    public class HoSo
    {
        public DateTime? ngayCapCmt;
        public DateTime? ngaySinh;
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DefNgaySinh { get; set; }
        public string MaNoiThuongTru { get; set; }
        public string NoiThuongTru { get; set; }
        public byte[] AnhChuKy { get; set; }
        public string SoHc { get; set; }
        public string NguoiCapNhatAnhChanDung { get; set; }
        public DateTime? NgayCapNhatAnhChanDung { get; set; }
        public string NguoiNhapMoiAnhChanDung { get; set; }
        public DateTime? NgayNhapMoiAnhChanDung { get; set; }
        public string SoDangKy { get; set; }
        public List<byte[]> AllAnhChanDung { get; set; }
        public string NoiSinhDayDu { get; }
        public string NoiSinh { get; set; }
        public string QgNoiSinh { get; set; }
        public string TenKd { get; set; }
        public byte[] AnhTrang2 { get; set; }
        public byte[] AnhTrang1 { get; set; }
        public string SoBienNhan { get; set; }
        public string MaHoSo { get; set; }
        public decimal? Id { get; set; }
        public string SoCmt { get; set; }
        public string Ten { get; set; }
        public string NgaySinhDayDu { get; set; }
        public string TenGioiTinh { get; set; }
        public byte[] AnhChanDung { get; set; }
        public string TinhNoiSinh { get; set; }
        public string SoMayTinh { get; set; }
        public bool CoAnh { get; }
        public bool CoNhieuAnh { get; }
        public bool DuAnhAbtc { get; }
        public bool CoTrangQuet { get; }
        public DateTime? NgayCapCmt { get; set; }
        public string NoiCapCmt { get; set; }
        public string LoaiToKhai { get; set; }
        public string SoMayTinhChinh { get; set; }
        public string SoTheThuongTru { get; set; }
        public string Base64 { get; set; }
    }
}