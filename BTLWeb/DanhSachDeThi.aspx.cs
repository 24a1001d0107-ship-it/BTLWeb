
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BTLWeb
{
    public partial class DanhSachDeThi : System.Web.UI.Page
    {
        // Lớp lưu thông tin đề thi
        public class DeThi
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string MonHoc { get; set; }
            public int ThoiGian { get; set; }
            public int SoCau { get; set; }
            public string MoTa { get; set; }
        }

        private List<DeThi> danhSachDeThi = new List<DeThi>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Yêu cầu đăng nhập
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiDanhSach("");
            }
        }

        // Đọc đề thi từ file TXT
        private List<DeThi> DocDanhSachDeThi()
        {
            List<DeThi> ds = new List<DeThi>();

            string path = Server.MapPath("~/App_Data/DeThi.txt");

            if (!File.Exists(path))
            {
                return ds;
            }

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] p = line.Split('|');

                // Mỗi dòng phải có đủ 7 trường
                if (p.Length != 7)
                    continue;

                int thoiGian;
                int soCau;

                if (!int.TryParse(p[3], out thoiGian))
                    continue;

                if (!int.TryParse(p[4], out soCau))
                    continue;

                // Chỉ lấy đề đang hoạt động
                if (p[6].Trim() != "1")
                    continue;

                ds.Add(new DeThi
                {
                    MaDe = p[0].Trim(),
                    TenDe = p[1].Trim(),
                    MonHoc = p[2].Trim(),
                    ThoiGian = thoiGian,
                    SoCau = soCau,
                    MoTa = p[5].Trim()
                });
            }

            return ds;
        }

        // Hiển thị danh sách và tìm kiếm
        private void HienThiDanhSach(string tuKhoa)
        {
            danhSachDeThi = DocDanhSachDeThi();

            List<DeThi> ketQua = danhSachDeThi;

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                ketQua = danhSachDeThi.Where(x =>
                    x.TenDe.IndexOf(
                        tuKhoa,
                        StringComparison.OrdinalIgnoreCase) >= 0
                    ||
                    x.MonHoc.IndexOf(
                        tuKhoa,
                        StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }

            rptDeThi.DataSource = ketQua;
            rptDeThi.DataBind();

            pnlKhongCoDe.Visible = ketQua.Count == 0;

            if (ketQua.Count > 0)
            {
                lblThongBao.Text =
                    "Tìm thấy " + ketQua.Count + " đề thi.";
            }
            else
            {
                lblThongBao.Text = "";
            }
        }

        // Tìm kiếm đề thi
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            HienThiDanhSach(tuKhoa);
        }

        // Hiện tất cả đề thi
        protected void btnHienTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";

            HienThiDanhSach("");
        }
    }
}
