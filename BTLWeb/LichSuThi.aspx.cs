
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLWeb
{
    public partial class LichSuThi : Page
    {
        private string DuongDanKetQua
        {
            get
            {
                return Server.MapPath("~/App_Data/KetQuaThi.txt");
            }
        }

        private string DuongDanDeThi
        {
            get
            {
                return Server.MapPath("~/App_Data/DeThi.txt");
            }
        }

        public class LichSu
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string ThoiGian { get; set; }

            public int TongCau { get; set; }
            public int DaTraLoi { get; set; }
            public int CauDung { get; set; }
            public int CauSai { get; set; }

            public double Diem { get; set; }
            public string XepLoai { get; set; }

            public DateTime NgayNop { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiLichSu("");
            }
        }

        private List<LichSu> DocLichSu()
        {
            List<LichSu> danhSach = new List<LichSu>();

            string tenDangNhap = Session["TenDangNhap"].ToString();

            if (!File.Exists(DuongDanKetQua))
                return danhSach;

            string[] dongDuLieu = File.ReadAllLines(DuongDanKetQua);

            foreach (string dong in dongDuLieu)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length != 8)
                    continue;

                // Chỉ lấy kết quả của tài khoản đang đăng nhập
                if (p[1] != tenDangNhap)
                    continue;

                int tongCau;
                int daTraLoi;
                int cauDung;
                double diem;

                if (!int.TryParse(p[4], out tongCau))
                    continue;

                if (!int.TryParse(p[5], out daTraLoi))
                    continue;

                if (!int.TryParse(p[6], out cauDung))
                    continue;

                if (!double.TryParse(
                    p[7],
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out diem))
                {
                    continue;
                }

                DateTime ngayNop;

                if (!DateTime.TryParseExact(
                    p[3],
                    "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ngayNop))
                {
                    continue;
                }

                string tenDe = LayTenDe(p[2]);

                int cauSai = daTraLoi - cauDung;

                if (cauSai < 0)
                    cauSai = 0;

                string xepLoai;

                if (diem >= 8)
                    xepLoai = "Xuất sắc";
                else if (diem >= 6.5)
                    xepLoai = "Khá";
                else if (diem >= 5)
                    xepLoai = "Trung bình";
                else
                    xepLoai = "Chưa đạt";

                LichSu item = new LichSu();

                item.MaDe = p[2];
                item.TenDe = tenDe;
                item.ThoiGian = p[3];

                item.TongCau = tongCau;
                item.DaTraLoi = daTraLoi;
                item.CauDung = cauDung;
                item.CauSai = cauSai;

                item.Diem = diem;
                item.XepLoai = xepLoai;
                item.NgayNop = ngayNop;

                danhSach.Add(item);
            }

            // Sắp xếp bài thi mới nhất lên đầu
            danhSach = danhSach
                .OrderByDescending(x => x.NgayNop)
                .ToList();

            return danhSach;
        }

        private string LayTenDe(string maDe)
        {
            if (!File.Exists(DuongDanDeThi))
                return maDe;

            string[] dongDuLieu = File.ReadAllLines(DuongDanDeThi);

            foreach (string dong in dongDuLieu)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length == 7 && p[0] == maDe)
                {
                    return p[1];
                }
            }

            return maDe;
        }

        private void HienThiLichSu(string tuKhoa)
        {
            List<LichSu> danhSach = DocLichSu();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                danhSach = danhSach
                    .Where(x =>
                        x.MaDe.IndexOf(
                            tuKhoa,
                            StringComparison.OrdinalIgnoreCase) >= 0
                        ||
                        x.TenDe.IndexOf(
                            tuKhoa,
                            StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            gvLichSu.DataSource = danhSach;
            gvLichSu.DataBind();

            if (danhSach.Count == 0)
            {
                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    lblThongBao.Text = "Bạn chưa có lịch sử thi nào.";
                }
                else
                {
                    lblThongBao.Text = "Không tìm thấy kết quả phù hợp.";
                }
            }
            else
            {
                lblThongBao.Text = "Bạn đã hoàn thành " +
                    danhSach.Count + " lượt thi.";
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            HienThiLichSu(txtTimKiem.Text.Trim());
        }

        protected void btnHienTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            HienThiLichSu("");
        }

        protected void gvLichSu_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = e.Row.FindControl("lblXepLoai") as Label;

                if (lbl != null)
                {
                    LichSu item = e.Row.DataItem as LichSu;

                    if (item != null)
                    {
                        if (item.Diem >= 8)
                            lbl.CssClass = "history-excellent";
                        else if (item.Diem >= 6.5)
                            lbl.CssClass = "history-good";
                        else if (item.Diem >= 5)
                            lbl.CssClass = "history-average";
                        else
                            lbl.CssClass = "history-poor";
                    }
                }
            }
        }
    }
}
