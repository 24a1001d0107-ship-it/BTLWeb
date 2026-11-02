
using System;
using System.Net.Mail;

namespace BTLWeb
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TenDangNhap"] != null)
                {
                    Response.Redirect("TrangChu.aspx");
                }
            }
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string username = txtTenDangNhap.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtMatKhau.Text;
            string nhapLai = txtNhapLaiMatKhau.Text;

            // Lấy vai trò được chọn trên giao diện
            string vaiTro = ddlVaiTro.SelectedValue;

            if (hoTen == "" || username == "" ||
                email == "" || password == "" || nhapLai == "")
            {
                HienThongBao("Vui lòng nhập đầy đủ thông tin.", false);
                return;
            }

            if (password.Length < 6)
            {
                HienThongBao("Mật khẩu phải có ít nhất 6 ký tự.", false);
                return;
            }

            if (password != nhapLai)
            {
                HienThongBao("Mật khẩu nhập lại không khớp.", false);
                return;
            }

            if (vaiTro != "Student" && vaiTro != "Teacher")
            {
                HienThongBao("Vai trò không hợp lệ.", false);
                return;
            }

            try
            {
                MailAddress diaChiEmail = new MailAddress(email);

                if (diaChiEmail.Address != email)
                {
                    HienThongBao("Email không hợp lệ.", false);
                    return;
                }
            }
            catch
            {
                HienThongBao("Email không hợp lệ.", false);
                return;
            }

            string path = Server.MapPath("~/App_Data/TaiKhoan.txt");

            bool ketQua = XuLyTaiKhoan.DangKy(
                path,
                username,
                password,
                vaiTro,
                hoTen,
                email
            );

            if (ketQua)
            {
                HienThongBao(
                    "Đăng ký thành công! Đang chuyển đến trang đăng nhập...",
                    true
                );

                Response.AddHeader("REFRESH", "2;URL=DangNhap.aspx");
            }
            else
            {
                HienThongBao(
                    "Đăng ký thất bại. Tên đăng nhập có thể đã tồn tại hoặc dữ liệu không hợp lệ.",
                    false
                );
            }
        }

        private void HienThongBao(string noiDung, bool thanhCong)
        {
            lblThongBao.Text = Server.HtmlEncode(noiDung);

            lblThongBao.CssClass = thanhCong
                ? "register-message success"
                : "register-message error";
        }
    }
}
