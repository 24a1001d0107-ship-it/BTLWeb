
using System;
using System.Net.Mail;

namespace BTLWeb
{
    public partial class TaiKhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiThongTin();
            }
        }

        // Hiển thị thông tin tài khoản
        private void HienThiThongTin()
        {
            string username = Session["TenDangNhap"].ToString();

            string hoTen = Session["HoTen"] == null
                ? username
                : Session["HoTen"].ToString();

            string email = Session["Email"] == null
                ? ""
                : Session["Email"].ToString();

            string vaiTro = Session["VaiTro"] == null
                ? ""
                : Session["VaiTro"].ToString();

            lblTenDangNhap.Text = Server.HtmlEncode(username);
            lblHoTenHienThi.Text = Server.HtmlEncode(hoTen);
            lblEmailHienThi.Text = Server.HtmlEncode(email);

            txtHoTen.Text = hoTen;
            txtEmail.Text = email;

            if (vaiTro == "Admin")
                lblVaiTro.Text = "Quản trị viên";
            else if (vaiTro == "Teacher")
                lblVaiTro.Text = "Giáo viên";
            else
                lblVaiTro.Text = "Học sinh";
        }

        // Cập nhật họ tên và email
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            string username = Session["TenDangNhap"].ToString();

            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (hoTen == "" || email == "")
            {
                HienThongBao(
                    lblThongBaoThongTin,
                    "Vui lòng nhập đầy đủ họ tên và email.",
                    false
                );
                return;
            }

            try
            {
                MailAddress diaChi = new MailAddress(email);

                if (diaChi.Address != email)
                {
                    HienThongBao(
                        lblThongBaoThongTin,
                        "Email không hợp lệ.",
                        false
                    );
                    return;
                }
            }
            catch
            {
                HienThongBao(
                    lblThongBaoThongTin,
                    "Email không hợp lệ.",
                    false
                );
                return;
            }

            string path = Server.MapPath("~/App_Data/TaiKhoan.txt");

            bool ketQua = XuLyTaiKhoan.CapNhatThongTin(
                path,
                username,
                hoTen,
                email
            );

            if (ketQua)
            {
                // Cập nhật Session để giao diện thay đổi ngay
                Session["HoTen"] = hoTen;
                Session["Email"] = email;

                HienThiThongTin();

                HienThongBao(
                    lblThongBaoThongTin,
                    "Cập nhật thông tin thành công!",
                    true
                );
            }
            else
            {
                HienThongBao(
                    lblThongBaoThongTin,
                    "Cập nhật thất bại. Vui lòng thử lại.",
                    false
                );
            }
        }

        // Đổi mật khẩu
        protected void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string nhapLai = txtNhapLaiMatKhau.Text;

            if (matKhauCu == "" ||
                matKhauMoi == "" ||
                nhapLai == "")
            {
                HienThongBao(
                    lblThongBaoMatKhau,
                    "Vui lòng nhập đầy đủ thông tin mật khẩu.",
                    false
                );
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                HienThongBao(
                    lblThongBaoMatKhau,
                    "Mật khẩu mới phải có ít nhất 6 ký tự.",
                    false
                );
                return;
            }

            if (matKhauMoi != nhapLai)
            {
                HienThongBao(
                    lblThongBaoMatKhau,
                    "Mật khẩu mới nhập lại không khớp.",
                    false
                );
                return;
            }

            if (matKhauCu == matKhauMoi)
            {
                HienThongBao(
                    lblThongBaoMatKhau,
                    "Mật khẩu mới phải khác mật khẩu hiện tại.",
                    false
                );
                return;
            }

            string username = Session["TenDangNhap"].ToString();
            string path = Server.MapPath("~/App_Data/TaiKhoan.txt");

            bool ketQua = XuLyTaiKhoan.DoiMatKhau(
                path,
                username,
                matKhauCu,
                matKhauMoi
            );

            if (ketQua)
            {
                txtMatKhauCu.Text = "";
                txtMatKhauMoi.Text = "";
                txtNhapLaiMatKhau.Text = "";

                HienThongBao(
                    lblThongBaoMatKhau,
                    "Đổi mật khẩu thành công!",
                    true
                );
            }
            else
            {
                HienThongBao(
                    lblThongBaoMatKhau,
                    "Mật khẩu hiện tại không chính xác hoặc có lỗi khi đổi mật khẩu.",
                    false
                );
            }
        }

        // Hiển thị thông báo
        private void HienThongBao(
            System.Web.UI.WebControls.Label label,
            string noiDung,
            bool thanhCong)
        {
            label.Text = Server.HtmlEncode(noiDung);

            label.CssClass = thanhCong
                ? "account-message success"
                : "account-message error";
        }
    }
}
