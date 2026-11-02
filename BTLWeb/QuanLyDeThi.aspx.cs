
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLWeb
{
    public partial class QuanLyDeThi : Page
    {
        private string DuongDan
        {
            get
            {
                return Server.MapPath("~/App_Data/DeThi.txt");
            }
        }

        public class DeThi
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string MonHoc { get; set; }
            public int ThoiGian { get; set; }
            public int SoCau { get; set; }
            public string MoTa { get; set; }
            public string TrangThai { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            string vaiTro = Convert.ToString(Session["VaiTro"]);

            if (vaiTro != "Teacher" && vaiTro != "Admin")
            {
                Response.Redirect("~/TrangChu.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiDanhSach("");
            }
        }

        private List<DeThi> DocDanhSach()
        {
            List<DeThi> danhSach = new List<DeThi>();

            if (!File.Exists(DuongDan))
                return danhSach;

            string[] cacDong = File.ReadAllLines(DuongDan);

            foreach (string dong in cacDong)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length != 7)
                    continue;

                int thoiGian;
                int soCau;

                if (!int.TryParse(p[3], out thoiGian))
                    continue;

                if (!int.TryParse(p[4], out soCau))
                    continue;

                DeThi de = new DeThi();

                de.MaDe = p[0];
                de.TenDe = p[1];
                de.MonHoc = p[2];
                de.ThoiGian = thoiGian;
                de.SoCau = soCau;
                de.MoTa = p[5];
                de.TrangThai = p[6];

                danhSach.Add(de);
            }

            return danhSach;
        }

        private void GhiDanhSach(List<DeThi> danhSach)
        {
            List<string> cacDong = new List<string>();

            foreach (DeThi de in danhSach)
            {
                string dong =
                    de.MaDe + "|" +
                    de.TenDe + "|" +
                    de.MonHoc + "|" +
                    de.ThoiGian + "|" +
                    de.SoCau + "|" +
                    de.MoTa + "|" +
                    de.TrangThai;

                cacDong.Add(dong);
            }

            File.WriteAllLines(DuongDan, cacDong);
        }

        private bool DuLieuHopLe(string giaTri)
        {
            return !string.IsNullOrWhiteSpace(giaTri)
                && !giaTri.Contains("|")
                && !giaTri.Contains("\r")
                && !giaTri.Contains("\n");
        }

        private void HienThiDanhSach(string tuKhoa)
        {
            List<DeThi> danhSach = DocDanhSach();

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

            gvDeThi.DataSource = danhSach;
            gvDeThi.DataBind();
        }

        private void XoaTrang()
        {
            hfMaDeCu.Value = "";
            txtMaDe.Text = "";
            txtTenDe.Text = "";
            txtMonHoc.Text = "";
            txtThoiGian.Text = "";
            txtSoCau.Text = "";
            txtMoTa.Text = "";
            ddlTrangThai.SelectedValue = "1";
        }

        private bool LayThongTinTuForm(out DeThi de)
        {
            de = new DeThi();

            string maDe = txtMaDe.Text.Trim();
            string tenDe = txtTenDe.Text.Trim();
            string monHoc = txtMonHoc.Text.Trim();
            string thoiGianText = txtThoiGian.Text.Trim();
            string soCauText = txtSoCau.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            int thoiGian;
            int soCau;

            if (!DuLieuHopLe(maDe) ||
                !DuLieuHopLe(tenDe) ||
                !DuLieuHopLe(monHoc) ||
                !DuLieuHopLe(moTa))
            {
                lblThongBao.Text =
                    "Vui lòng nhập đầy đủ thông tin và không sử dụng ký tự |.";
                return false;
            }

            if (!int.TryParse(thoiGianText, out thoiGian)
                || thoiGian <= 0)
            {
                lblThongBao.Text = "Thời gian phải là số nguyên lớn hơn 0.";
                return false;
            }

            if (!int.TryParse(soCauText, out soCau)
                || soCau <= 0)
            {
                lblThongBao.Text = "Số câu phải là số nguyên lớn hơn 0.";
                return false;
            }

            de.MaDe = maDe;
            de.TenDe = tenDe;
            de.MonHoc = monHoc;
            de.ThoiGian = thoiGian;
            de.SoCau = soCau;
            de.MoTa = moTa;
            de.TrangThai = ddlTrangThai.SelectedValue;

            return true;
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            DeThi de;

            if (!LayThongTinTuForm(out de))
                return;

            List<DeThi> danhSach = DocDanhSach();

            foreach (DeThi item in danhSach)
            {
                if (item.MaDe.Equals(
                    de.MaDe,
                    StringComparison.OrdinalIgnoreCase))
                {
                    lblThongBao.Text = "Mã đề thi đã tồn tại.";
                    return;
                }
            }

            // Kiểm tra ký tự phân cách trong dữ liệu
            danhSach.Add(de);

            GhiDanhSach(danhSach);

            lblThongBao.Text = "Thêm đề thi thành công!";

            XoaTrang();
            HienThiDanhSach("");
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfMaDeCu.Value))
            {
                lblThongBao.Text = "Vui lòng chọn đề thi cần cập nhật.";
                return;
            }

            DeThi de;

            if (!LayThongTinTuForm(out de))
                return;

            List<DeThi> danhSach = DocDanhSach();

            int viTri = danhSach.FindIndex(
                x => x.MaDe == hfMaDeCu.Value);

            if (viTri < 0)
            {
                lblThongBao.Text = "Không tìm thấy đề thi cần cập nhật.";
                return;
            }

            for (int i = 0; i < danhSach.Count; i++)
            {
                if (i != viTri &&
                    danhSach[i].MaDe.Equals(
                        de.MaDe,
                        StringComparison.OrdinalIgnoreCase))
                {
                    lblThongBao.Text = "Mã đề mới đã được sử dụng.";
                    return;
                }
            }

            // Không đổi mã đề nếu đề đã có câu hỏi.
            if (de.MaDe != hfMaDeCu.Value && CoCauHoi(hfMaDeCu.Value))
            {
                lblThongBao.Text =
                    "Đề thi đã có câu hỏi nên không thể đổi mã đề.";
                return;
            }

            danhSach[viTri] = de;

            GhiDanhSach(danhSach);

            lblThongBao.Text = "Cập nhật đề thi thành công!";

            XoaTrang();
            HienThiDanhSach("");
        }

        private bool CoCauHoi(string maDe)
        {
            string duongDanCauHoi =
                Server.MapPath("~/App_Data/CauHoi.txt");

            if (!File.Exists(duongDanCauHoi))
                return false;

            string[] cacDong = File.ReadAllLines(duongDanCauHoi);

            foreach (string dong in cacDong)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length == 8 && p[1] == maDe)
                    return true;
            }

            return false;
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            lblThongBao.Text = "";
            HienThiDanhSach("");
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            HienThiDanhSach(txtTimKiem.Text.Trim());
        }

        protected void btnHienTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            HienThiDanhSach("");
        }

        protected void gvDeThi_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            string maDe = Convert.ToString(e.CommandArgument);

            List<DeThi> danhSach = DocDanhSach();

            DeThi de = danhSach.Find(x => x.MaDe == maDe);

            if (de == null)
            {
                lblThongBao.Text = "Không tìm thấy đề thi.";
                return;
            }

            if (e.CommandName == "SuaDe")
            {
                hfMaDeCu.Value = de.MaDe;

                txtMaDe.Text = de.MaDe;
                txtTenDe.Text = de.TenDe;
                txtMonHoc.Text = de.MonHoc;
                txtThoiGian.Text = de.ThoiGian.ToString();
                txtSoCau.Text = de.SoCau.ToString();
                txtMoTa.Text = de.MoTa;
                ddlTrangThai.SelectedValue = de.TrangThai;

                lblThongBao.Text = "Đang chỉnh sửa đề thi: " + de.MaDe;
            }
            else if (e.CommandName == "AnHienDe")
            {
                de.TrangThai = de.TrangThai == "1" ? "0" : "1";

                GhiDanhSach(danhSach);

                lblThongBao.Text = de.TrangThai == "1"
                    ? "Đã mở lại đề thi."
                    : "Đã ẩn đề thi.";

                HienThiDanhSach(txtTimKiem.Text.Trim());
            }
        }
    }
}
