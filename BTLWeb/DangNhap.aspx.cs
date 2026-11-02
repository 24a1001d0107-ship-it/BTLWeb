
using System;

namespace BTLWeb
{
    public partial class DangNhap : System.Web.UI.Page
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

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;

            if (username == "" || password == "")
            {
                HienThongBao("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            string path = Server.MapPath("~/App_Data/TaiKhoan.txt");

            string hoTen;
            string email;
            string vaiTro;

            bool hopLe = XuLyTaiKhoan.DangNhap(
                path,
                username,
                password,
                out hoTen,
                out email,
                out vaiTro
            );

            if (!hopLe)
            {
                HienThongBao("Tên đăng nhập hoặc mật khẩu không chính xác.");
                return;
            }

            // Xóa Session cũ trước khi tạo Session mới
            Session.Clear();

            // Lưu thông tin tài khoản
            Session["TenDangNhap"] = username;
            Session["HoTen"] = hoTen;
            Session["Email"] = email;
            Session["VaiTro"] = vaiTro;

            // Chuyển trang theo quyền
            if (vaiTro == "Admin" || vaiTro == "Teacher")
            {
                Response.Redirect("QuanLyDeThi.aspx");
            }
            else if (vaiTro == "Student")
            {
                Response.Redirect("DanhSachDeThi.aspx");
            }
            else
            {
                Session.Clear();
                HienThongBao("Tài khoản chưa được cấp vai trò hợp lệ.");
            }
        }

        private void HienThongBao(string noiDung)
        {
            lblThongBao.Text = Server.HtmlEncode(noiDung);
            lblThongBao.CssClass = "login-message error";
        }
    }
}
