
<%@ Page Title="Lịch sử thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="LichSuThi.aspx.cs"
    Inherits="BTLWeb.LichSuThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/LichSuThi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="history-page">

        <div class="history-header">
            <h1>LỊCH SỬ THI</h1>
            <p>Xem lại kết quả những bài thi bạn đã hoàn thành</p>
        </div>

        <div class="history-search">
            <asp:TextBox ID="txtTimKiem" runat="server"
                CssClass="history-search-input"
                placeholder="Nhập tên hoặc mã đề thi...">
            </asp:TextBox>

            <asp:Button ID="btnTimKiem" runat="server"
                Text="Tìm kiếm"
                CssClass="history-search-button"
                OnClick="btnTimKiem_Click" />

            <asp:Button ID="btnHienTatCa" runat="server"
                Text="Hiện tất cả"
                CssClass="history-reset-button"
                OnClick="btnHienTatCa_Click" />
        </div>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="history-message">
        </asp:Label>

        <div class="history-table-wrapper">

            <asp:GridView ID="gvLichSu" runat="server"
                AutoGenerateColumns="False"
                CssClass="history-table"
                GridLines="None"
                EmptyDataText="Bạn chưa có bài thi nào."
                OnRowDataBound="gvLichSu_RowDataBound">

                <Columns>
                    <asp:BoundField DataField="MaDe"
                        HeaderText="Mã đề" />

                    <asp:BoundField DataField="TenDe"
                        HeaderText="Tên đề thi" />

                    <asp:BoundField DataField="ThoiGian"
                        HeaderText="Thời gian nộp" />

                    <asp:BoundField DataField="TongCau"
                        HeaderText="Tổng câu" />

                    <asp:BoundField DataField="DaTraLoi"
                        HeaderText="Đã trả lời" />

                    <asp:BoundField DataField="CauDung"
                        HeaderText="Câu đúng" />

                    <asp:BoundField DataField="CauSai"
                        HeaderText="Câu sai" />

                    <asp:BoundField DataField="Diem"
                        HeaderText="Điểm" />

                    <asp:TemplateField HeaderText="Kết quả">
                        <ItemTemplate>
                            <asp:Label ID="lblXepLoai"
                                runat="server"
                                Text='<%# Eval("XepLoai") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>

        <div class="history-footer">
            <asp:HyperLink ID="lnkDanhSachDe" runat="server"
                NavigateUrl="~/DanhSachDeThi.aspx"
                CssClass="history-back-button">
                ← Quay lại danh sách đề thi
            </asp:HyperLink>
        </div>

    </div>

</asp:Content>
