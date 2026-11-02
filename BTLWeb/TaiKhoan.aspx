
<%@ Page Title="Tài khoản cá nhân" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="TaiKhoan.aspx.cs"
    Inherits="BTLWeb.TaiKhoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/TaiKhoan.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="account-container">

        <div class="account-heading">
            <h1>Thông tin tài khoản</h1>
            <p>Quản lý thông tin cá nhân và bảo mật tài khoản</p>
        </div>

        <div class="account-card">

            <div class="account-avatar">👤</div>

            <h2>
                <asp:Label ID="lblHoTenHienThi" runat="server" />
            </h2>

            <span class="account-role">
                <asp:Label ID="lblVaiTro" runat="server" />
            </span>

            <div class="account-info">
                <div class="info-row">
                    <span>Tên đăng nhập:</span>
                    <asp:Label ID="lblTenDangNhap" runat="server" />
                </div>

                <div class="info-row">
                    <span>Email:</span>
                    <asp:Label ID="lblEmailHienThi" runat="server" />
                </div>
            </div>

        </div>

        <div class="account-form">

            <h2>Cập nhật thông tin cá nhân</h2>

            <div class="form-group">
                <label>Họ và tên</label>

                <asp:TextBox ID="txtHoTen" runat="server"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Email</label>

                <asp:TextBox ID="txtEmail" runat="server"
                    TextMode="Email"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblThongBaoThongTin" runat="server"
                CssClass="account-message">
            </asp:Label>

            <asp:Button ID="btnCapNhat" runat="server"
                Text="LƯU THÔNG TIN"
                CssClass="account-button"
                OnClick="btnCapNhat_Click" />

        </div>

        <div class="account-form password-form">

            <h2>Đổi mật khẩu</h2>

            <div class="form-group">
                <label>Mật khẩu hiện tại</label>

                <asp:TextBox ID="txtMatKhauCu" runat="server"
                    TextMode="Password"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Mật khẩu mới</label>

                <asp:TextBox ID="txtMatKhauMoi" runat="server"
                    TextMode="Password"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label>Nhập lại mật khẩu mới</label>

                <asp:TextBox ID="txtNhapLaiMatKhau" runat="server"
                    TextMode="Password"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblThongBaoMatKhau" runat="server"
                CssClass="account-message">
            </asp:Label>

            <asp:Button ID="btnDoiMatKhau" runat="server"
                Text="ĐỔI MẬT KHẨU"
                CssClass="account-button"
                OnClick="btnDoiMatKhau_Click" />

        </div>

    </div>

</asp:Content>
