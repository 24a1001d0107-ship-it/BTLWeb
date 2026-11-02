
<%@ Page Title="Đăng ký" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DangKy.aspx.cs"
    Inherits="BTLWeb.DangKy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DangKy.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="register-container">

        <div class="register-heading">
            <h1>Tạo tài khoản</h1>
            <p>Đăng ký để tham gia thi trắc nghiệm trực tuyến</p>
        </div>

        <div class="register-form">

            <div class="form-group">
                <label>Họ và tên</label>

                <asp:TextBox ID="txtHoTen" runat="server"
                    CssClass="form-control"
                    placeholder="Nhập họ và tên">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Tên đăng nhập</label>

                <asp:TextBox ID="txtTenDangNhap" runat="server"
                    CssClass="form-control"
                    placeholder="Nhập tên đăng nhập">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Email</label>

                <asp:TextBox ID="txtEmail" runat="server"
                    TextMode="Email"
                    CssClass="form-control"
                    placeholder="Nhập địa chỉ email">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Loại tài khoản</label>

                <asp:DropDownList ID="ddlVaiTro" runat="server"
                    CssClass="form-control">

                    <asp:ListItem Text="Học sinh"
                        Value="Student" />

                    <asp:ListItem Text="Giáo viên"
                        Value="Teacher" />

                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Mật khẩu</label>

                <asp:TextBox ID="txtMatKhau" runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    placeholder="Nhập mật khẩu">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Nhập lại mật khẩu</label>

                <asp:TextBox ID="txtNhapLaiMatKhau" runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    placeholder="Nhập lại mật khẩu">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="register-message">
            </asp:Label>

            <asp:Button ID="btnDangKy" runat="server"
                Text="ĐĂNG KÝ"
                CssClass="register-button"
                OnClick="btnDangKy_Click" />

            <div class="register-footer">
                Đã có tài khoản?
                <a href="DangNhap.aspx">Đăng nhập ngay</a>
            </div>

        </div>
    </div>

</asp:Content>
