
<%@ Page Title="Đăng nhập" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DangNhap.aspx.cs"
    Inherits="BTLWeb.DangNhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DangNhap.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="login-container">

        <div class="login-heading">
            <div class="login-icon">🔐</div>
            <h1>Đăng nhập</h1>
            <p>Đăng nhập để tiếp tục sử dụng hệ thống</p>
        </div>

        <div class="login-form">

            <div class="form-group">
                <label>Tên đăng nhập</label>

                <asp:TextBox ID="txtTenDangNhap" runat="server"
                    CssClass="form-control"
                    placeholder="Nhập tên đăng nhập">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Mật khẩu</label>

                <asp:TextBox ID="txtMatKhau" runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    placeholder="Nhập mật khẩu">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="login-message">
            </asp:Label>

            <asp:Button ID="btnDangNhap" runat="server"
                Text="ĐĂNG NHẬP"
                CssClass="login-button"
                OnClick="btnDangNhap_Click" />

            <div class="login-footer">
                Chưa có tài khoản?
                <a href="DangKy.aspx">Đăng ký ngay</a>
            </div>

        </div>

    </div>

</asp:Content>
