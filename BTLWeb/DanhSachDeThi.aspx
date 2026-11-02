
<%@ Page Title="Danh sách đề thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DanhSachDeThi.aspx.cs"
    Inherits="BTLWeb.DanhSachDeThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DanhSachDeThi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="exam-page">

        <div class="exam-heading">
            <h1>Danh sách đề thi</h1>
            <p>Lựa chọn đề thi phù hợp để kiểm tra kiến thức của bạn.</p>
        </div>

        <div class="exam-search">

            <asp:TextBox ID="txtTimKiem" runat="server"
                CssClass="search-input"
                placeholder="Nhập tên đề thi hoặc môn học...">
            </asp:TextBox>

            <asp:Button ID="btnTimKiem" runat="server"
                Text="Tìm kiếm"
                CssClass="search-button"
                OnClick="btnTimKiem_Click" />

            <asp:Button ID="btnHienTatCa" runat="server"
                Text="Hiện tất cả"
                CssClass="show-all-button"
                OnClick="btnHienTatCa_Click" />

        </div>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="exam-message">
        </asp:Label>

        <div class="exam-list">

            <asp:Repeater ID="rptDeThi" runat="server">

                <ItemTemplate>

                    <div class="exam-card">

                        <div class="exam-card-top">
                            <span class="exam-subject">
                                <%#: Eval("MonHoc") %>
                            </span>

                            <span class="exam-status">
                                Đang hoạt động
                            </span>
                        </div>

                        <h2><%#: Eval("TenDe") %></h2>

                        <p class="exam-description">
                            <%#: Eval("MoTa") %>
                        </p>

                        <div class="exam-information">

                            <div class="exam-info-item">
                                <span>⏱</span>
                                <div>
                                    <strong><%#: Eval("ThoiGian") %> phút</strong>
                                    <small>Thời gian</small>
                                </div>
                            </div>

                            <div class="exam-info-item">
                                <span>📝</span>
                                <div>
                                    <strong><%#: Eval("SoCau") %> câu</strong>
                                    <small>Số lượng câu hỏi</small>
                                </div>
                            </div>

                        </div>

                        <div class="exam-card-bottom">

                            <span class="exam-code">
                                Mã đề: <%#: Eval("MaDe") %>
                            </span>

                            <a class="start-exam-button"
                               href='<%# "LamBaiThi.aspx?MaDe=" +
                                   Server.UrlEncode(Eval("MaDe").ToString()) %>'>
                                Bắt đầu thi
                            </a>

                        </div>

                    </div>

                </ItemTemplate>

            </asp:Repeater>

        </div>

        <asp:Panel ID="pnlKhongCoDe" runat="server"
            CssClass="no-exam" Visible="false">

            <div class="no-exam-icon">📭</div>

            <h3>Không tìm thấy đề thi</h3>

            <p>Hiện tại chưa có đề thi phù hợp với nội dung tìm kiếm.</p>

        </asp:Panel>

    </div>

</asp:Content>
