
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLWeb
{
    public partial class QuanLyCauHoi : Page
    {
        private string DuongDanCauHoi
        {
            get
            {
                return Server.MapPath("~/App_Data/CauHoi.txt");
            }
        }

        private string DuongDanDeThi
        {
            get
            {
                return Server.MapPath("~/App_Data/DeThi.txt");
            }
        }

        public class CauHoi
        {
            public string MaCauHoi { get; set; }
            public string MaDe { get; set; }
            public string NoiDung { get; set; }

            public string DapAnA { get; set; }
            public string DapAnB { get; set; }
            public string DapAnC { get; set; }
            public string DapAnD { get; set; }

            public string DapAnDung { get; set; }
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
                NapDanhSachDeThi();
                NapDanhSachLoc();
                HienThiDanhSach("", "");
            }
        }

        // Đọc danh sách đề thi
        private List<string[]> DocDeThi()
        {
            List<string[]> danhSach = new List<string[]>();

            if (!File.Exists(DuongDanDeThi))
                return danhSach;

            string[] cacDong = File.ReadAllLines(DuongDanDeThi);

            foreach (string dong in cacDong)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length == 7)
                    danhSach.Add(p);
            }

            return danhSach;
        }

        // Đổ danh sách đề thi vào DropDownList
        private void NapDanhSachDeThi()
        {
            ddlMaDe.Items.Clear();

            List<string[]> danhSach = DocDeThi();

            foreach (string[] p in danhSach)
            {
                ListItem item = new ListItem(
                    p[0] + " - " + p[1],
                    p[0]
                );

                ddlMaDe.Items.Add(item);
            }
        }

        // Đổ danh sách đề thi vào bộ lọc
        private void NapDanhSachLoc()
        {
            ddlLocMaDe.Items.Clear();

            ddlLocMaDe.Items.Add(
                new ListItem("Tất cả đề thi", "")
            );

            List<string[]> danhSach = DocDeThi();

            foreach (string[] p in danhSach)
            {
                ddlLocMaDe.Items.Add(
                    new ListItem(p[0] + " - " + p[1], p[0])
                );
            }
        }

        // Đọc danh sách câu hỏi
        private List<CauHoi> DocDanhSachCauHoi()
        {
            List<CauHoi> danhSach = new List<CauHoi>();

            if (!File.Exists(DuongDanCauHoi))
                return danhSach;

            string[] cacDong = File.ReadAllLines(DuongDanCauHoi);

            foreach (string dong in cacDong)
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length != 8)
                    continue;

                CauHoi cauHoi = new CauHoi();

                cauHoi.MaCauHoi = p[0];
                cauHoi.MaDe = p[1];
                cauHoi.NoiDung = p[2];
                cauHoi.DapAnA = p[3];
                cauHoi.DapAnB = p[4];
                cauHoi.DapAnC = p[5];
                cauHoi.DapAnD = p[6];
                cauHoi.DapAnDung = p[7];

                danhSach.Add(cauHoi);
            }

            return danhSach;
        }

        // Ghi danh sách câu hỏi xuống file
        private void GhiDanhSachCauHoi(List<CauHoi> danhSach)
        {
            List<string> cacDong = new List<string>();

            foreach (CauHoi c in danhSach)
            {
                string dong =
                    c.MaCauHoi + "|" +
                    c.MaDe + "|" +
                    c.NoiDung + "|" +
                    c.DapAnA + "|" +
                    c.DapAnB + "|" +
                    c.DapAnC + "|" +
                    c.DapAnD + "|" +
                    c.DapAnDung;

                cacDong.Add(dong);
            }

            File.WriteAllLines(DuongDanCauHoi, cacDong);
        }

        // Kiểm tra dữ liệu hợp lệ
        private bool HopLe(string giaTri)
        {
            return !string.IsNullOrWhiteSpace(giaTri)
                && !giaTri.Contains("|")
                && !giaTri.Contains("\r")
                && !giaTri.Contains("\n");
        }

        // Lấy thông tin từ form
        private bool LayThongTinForm(out CauHoi cauHoi)
        {
            cauHoi = new CauHoi();

            string maCauHoi = txtMaCauHoi.Text.Trim();
            string maDe = ddlMaDe.SelectedValue;
            string noiDung = txtNoiDung.Text.Trim();

            string a = txtDapAnA.Text.Trim();
            string b = txtDapAnB.Text.Trim();
            string c = txtDapAnC.Text.Trim();
            string d = txtDapAnD.Text.Trim();

            if (!HopLe(maCauHoi) ||
                !HopLe(maDe) ||
                !HopLe(noiDung) ||
                !HopLe(a) ||
                !HopLe(b) ||
                !HopLe(c) ||
                !HopLe(d))
            {
                lblThongBao.Text =
                    "Vui lòng nhập đầy đủ thông tin và không sử dụng ký tự |.";
                return false;
            }

            if (ddlDapAnDung.SelectedValue != "A" &&
                ddlDapAnDung.SelectedValue != "B" &&
                ddlDapAnDung.SelectedValue != "C" &&
                ddlDapAnDung.SelectedValue != "D")
            {
                lblThongBao.Text = "Đáp án đúng không hợp lệ.";
                return false;
            }

            cauHoi.MaCauHoi = maCauHoi;
            cauHoi.MaDe = maDe;
            cauHoi.NoiDung = noiDung;

            cauHoi.DapAnA = a;
            cauHoi.DapAnB = b;
            cauHoi.DapAnC = c;
            cauHoi.DapAnD = d;

            cauHoi.DapAnDung = ddlDapAnDung.SelectedValue;

            return true;
        }

        // Hiển thị danh sách
        private void HienThiDanhSach(string tuKhoa, string maDe)
        {
            List<CauHoi> danhSach = DocDanhSachCauHoi();

            if (!string.IsNullOrWhiteSpace(maDe))
            {
                danhSach = danhSach
                    .Where(x => x.MaDe == maDe)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                danhSach = danhSach
                    .Where(x =>
                        x.MaCauHoi.IndexOf(
                            tuKhoa,
                            StringComparison.OrdinalIgnoreCase) >= 0
                        ||
                        x.NoiDung.IndexOf(
                            tuKhoa,
                            StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            gvCauHoi.DataSource = danhSach;
            gvCauHoi.DataBind();
        }

        // Xóa dữ liệu trên form
        private void XoaTrang()
        {
            hfMaCauHoiCu.Value = "";

            txtMaCauHoi.Text = "";
            txtNoiDung.Text = "";

            txtDapAnA.Text = "";
            txtDapAnB.Text = "";
            txtDapAnC.Text = "";
            txtDapAnD.Text = "";

            if (ddlMaDe.Items.Count > 0)
                ddlMaDe.SelectedIndex = 0;

            ddlDapAnDung.SelectedValue = "A";
        }

        // Thêm câu hỏi
        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (ddlMaDe.Items.Count == 0)
            {
                lblThongBao.Text = "Bạn cần tạo đề thi trước khi thêm câu hỏi.";
                return;
            }

            CauHoi cauHoi;

            if (!LayThongTinForm(out cauHoi))
                return;

            List<CauHoi> danhSach = DocDanhSachCauHoi();

            foreach (CauHoi c in danhSach)
            {
                if (c.MaCauHoi.Equals(
                    cauHoi.MaCauHoi,
                    StringComparison.OrdinalIgnoreCase))
                {
                    lblThongBao.Text = "Mã câu hỏi đã tồn tại.";
                    return;
                }
            }

            danhSach.Add(cauHoi);

            GhiDanhSachCauHoi(danhSach);

            lblThongBao.Text = "Thêm câu hỏi thành công!";

            XoaTrang();

            HienThiDanhSach(
                txtTimKiem.Text.Trim(),
                ddlLocMaDe.SelectedValue
            );
        }

        // Cập nhật câu hỏi
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfMaCauHoiCu.Value))
            {
                lblThongBao.Text = "Vui lòng chọn câu hỏi cần cập nhật.";
                return;
            }

            CauHoi cauHoi;

            if (!LayThongTinForm(out cauHoi))
                return;

            List<CauHoi> danhSach = DocDanhSachCauHoi();

            int viTri = danhSach.FindIndex(
                x => x.MaCauHoi == hfMaCauHoiCu.Value
            );

            if (viTri < 0)
            {
                lblThongBao.Text = "Không tìm thấy câu hỏi cần sửa.";
                return;
            }

            for (int i = 0; i < danhSach.Count; i++)
            {
                if (i != viTri &&
                    danhSach[i].MaCauHoi.Equals(
                        cauHoi.MaCauHoi,
                        StringComparison.OrdinalIgnoreCase))
                {
                    lblThongBao.Text = "Mã câu hỏi đã được sử dụng.";
                    return;
                }
            }

            danhSach[viTri] = cauHoi;

            GhiDanhSachCauHoi(danhSach);

            lblThongBao.Text = "Cập nhật câu hỏi thành công!";

            XoaTrang();

            HienThiDanhSach(
                txtTimKiem.Text.Trim(),
                ddlLocMaDe.SelectedValue
            );
        }

        // Làm mới form
        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            lblThongBao.Text = "";

            HienThiDanhSach(
                txtTimKiem.Text.Trim(),
                ddlLocMaDe.SelectedValue
            );
        }

        // Tìm kiếm câu hỏi
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            HienThiDanhSach(
                txtTimKiem.Text.Trim(),
                ddlLocMaDe.SelectedValue
            );
        }

        // Hiện tất cả câu hỏi
        protected void btnHienTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            ddlLocMaDe.SelectedValue = "";

            HienThiDanhSach("", "");
        }

        // Lọc theo đề thi
        protected void ddlLocMaDe_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            HienThiDanhSach(
                txtTimKiem.Text.Trim(),
                ddlLocMaDe.SelectedValue
            );
        }

        // Xử lý nút Sửa và Xóa
        protected void gvCauHoi_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            string maCauHoi = Convert.ToString(e.CommandArgument);

            List<CauHoi> danhSach = DocDanhSachCauHoi();

            CauHoi cauHoi = danhSach.Find(
                x => x.MaCauHoi == maCauHoi
            );

            if (cauHoi == null)
            {
                lblThongBao.Text = "Không tìm thấy câu hỏi.";
                return;
            }

            if (e.CommandName == "SuaCauHoi")
            {
                hfMaCauHoiCu.Value = cauHoi.MaCauHoi;

                txtMaCauHoi.Text = cauHoi.MaCauHoi;
                txtNoiDung.Text = cauHoi.NoiDung;

                txtDapAnA.Text = cauHoi.DapAnA;
                txtDapAnB.Text = cauHoi.DapAnB;
                txtDapAnC.Text = cauHoi.DapAnC;
                txtDapAnD.Text = cauHoi.DapAnD;

                if (ddlMaDe.Items.FindByValue(cauHoi.MaDe) != null)
                    ddlMaDe.SelectedValue = cauHoi.MaDe;

                ddlDapAnDung.SelectedValue = cauHoi.DapAnDung;

                lblThongBao.Text =
                    "Đang chỉnh sửa câu hỏi: " + cauHoi.MaCauHoi;
            }
            else if (e.CommandName == "XoaCauHoi")
            {
                danhSach.Remove(cauHoi);

                GhiDanhSachCauHoi(danhSach);

                lblThongBao.Text = "Xóa câu hỏi thành công!";

                if (hfMaCauHoiCu.Value == maCauHoi)
                    XoaTrang();

                HienThiDanhSach(
                    txtTimKiem.Text.Trim(),
                    ddlLocMaDe.SelectedValue
                );
            }
        }
    }
}
