
using System;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace BTLWeb
{
    public partial class KetQuaThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiKetQua();
            }
        }

        // Hiển thị kết quả của lượt thi vừa hoàn thành
        private void HienThiKetQua()
        {
            string maKetQua =
                Session["MaKetQua"] == null
                ? ""
                : Session["MaKetQua"].ToString();

            string username = Session["TenDangNhap"].ToString();

            if (string.IsNullOrWhiteSpace(maKetQua))
            {
                HienThiLoi("Không tìm thấy thông tin kết quả thi.");
                return;
            }

            string path =
                Server.MapPath("~/App_Data/KetQuaThi.txt");

            if (!File.Exists(path))
            {
                HienThiLoi("File lưu kết quả thi không tồn tại.");
                return;
            }

            string[] thongTinKetQua = null;

            foreach (string dong in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length != 8)
                    continue;

                // Chỉ lấy kết quả của tài khoản đang đăng nhập
                if (p[0].Trim() == maKetQua &&
                    p[1].Trim() == username)
                {
                    thongTinKetQua = p;
                    break;
                }
            }

            if (thongTinKetQua == null)
            {
                HienThiLoi("Không tìm thấy kết quả của bài thi này.");
                return;
            }

            int tongCau;
            int daTraLoi;
            int cauDung;
            double diem;

            if (!int.TryParse(thongTinKetQua[4], out tongCau) ||
                !int.TryParse(thongTinKetQua[5], out daTraLoi) ||
                !int.TryParse(thongTinKetQua[6], out cauDung) ||
                !double.TryParse(
                    thongTinKetQua[7],
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out diem))
            {
                HienThiLoi("Dữ liệu kết quả thi không hợp lệ.");
                return;
            }

            int cauSai = daTraLoi - cauDung;
            int boTrong = tongCau - daTraLoi;

            if (cauSai < 0 || boTrong < 0)
            {
                HienThiLoi("Dữ liệu thống kê kết quả không hợp lệ.");
                return;
            }

            // Hiển thị điểm
            lblDiem.Text = diem.ToString("0.##");

            // Hiển thị mã đề
            lblMaDe.Text =
                Server.HtmlEncode(thongTinKetQua[2]);

            // Hiển thị thời gian nộp bài
            lblThoiGian.Text =
                Server.HtmlEncode(thongTinKetQua[3]);

            // Hiển thị thống kê
            lblTongCau.Text = tongCau.ToString();
            lblDaTraLoi.Text = daTraLoi.ToString();
            lblCauDung.Text = cauDung.ToString();
            lblCauSai.Text = cauSai.ToString();
            lblBoTrong.Text = boTrong.ToString();

            // Tìm tên đề thi
            lblTenDe.Text = Server.HtmlEncode(
                LayTenDeThi(thongTinKetQua[2])
            );

            // Đánh giá kết quả
            if (diem >= 8)
            {
                lblDanhGia.Text = "Xuất sắc! Bạn đã làm rất tốt.";
                lblDanhGia.CssClass = "result-evaluation excellent";
            }
            else if (diem >= 6.5)
            {
                lblDanhGia.Text = "Khá tốt! Hãy tiếp tục cố gắng.";
                lblDanhGia.CssClass = "result-evaluation good";
            }
            else if (diem >= 5)
            {
                lblDanhGia.Text = "Đạt yêu cầu. Bạn nên ôn tập thêm.";
                lblDanhGia.CssClass = "result-evaluation average";
            }
            else
            {
                lblDanhGia.Text = "Chưa đạt. Hãy cố gắng hơn ở lần sau.";
                lblDanhGia.CssClass = "result-evaluation poor";
            }

            pnlKetQua.Visible = true;
            pnlThongBao.Visible = false;
        }

        // Lấy tên đề thi từ DeThi.txt
        private string LayTenDeThi(string maDe)
        {
            string path =
                Server.MapPath("~/App_Data/DeThi.txt");

            if (!File.Exists(path))
                return maDe;

            foreach (string dong in File.ReadAllLines(path))
            {
                string[] p = dong.Split('|');

                if (p.Length == 7 &&
                    p[0].Trim() == maDe)
                {
                    return p[1].Trim();
                }
            }

            return maDe;
        }

        // Hiển thị thông báo khi không tìm thấy kết quả
        private void HienThiLoi(string noiDung)
        {
            pnlKetQua.Visible = false;
            pnlThongBao.Visible = true;
            lblThongBao.Text = Server.HtmlEncode(noiDung);
        }
    }
}
