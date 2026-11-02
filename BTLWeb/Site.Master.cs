
using System;

namespace BTLWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool daDangNhap = Session["TenDangNhap"] != null;

            phChuaDangNhap.Visible = !daDangNhap;
            phDaDangNhap.Visible = daDangNhap;

            string vaiTro = "";

            if (Session["VaiTro"] != null)
            {
                vaiTro = Session["VaiTro"].ToString();
            }

            // Giáo viên và Admin được nhìn thấy menu quản lý
            phQuanLy.Visible = daDangNhap &&
                (vaiTro == "Teacher" || vaiTro == "Admin");

            if (daDangNhap)
            {
                string hoTen = Session["HoTen"] == null
                    ? Session["TenDangNhap"].ToString()
                    : Session["HoTen"].ToString();

                lblTenNguoiDung.Text =
                    Server.HtmlEncode(hoTen);
            }
        }

        protected void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("TrangChu.aspx");
        }
    }
}
