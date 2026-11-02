
using System;

namespace BTLWeb
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                phChuaDangNhap.Visible =
                    Session["TenDangNhap"] == null;
            }
        }
    }
}
