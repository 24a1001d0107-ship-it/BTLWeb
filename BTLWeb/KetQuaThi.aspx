
<%@ Page Title="Kết quả thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="KetQuaThi.aspx.cs"
    Inherits="BTLWeb.KetQuaThi" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="HeadContent"
    runat="server">

    <link href="CSS/KetQuaThi.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="result-page">

        <div class="result-header">
            <div class="result-icon">
                <span>✓</span>
            </div>

            <h1>KẾT QUẢ BÀI THI</h1>

            <p>Bạn đã hoàn thành bài thi của mình.</p>
        </div>

        <asp:Panel ID="pnlKetQua" runat="server">

            <div class="result-score-card">

                <span class="score-caption">ĐIỂM SỐ CỦA BẠN</span>

                <div class="score-number">
                    <asp:Label ID="lblDiem" runat="server" />
                    <span>/ 10</span>
                </div>

                <asp:Label ID="lblDanhGia"
                    runat="server"
                    CssClass="result-evaluation">
                </asp:Label>

            </div>

            <div class="result-exam-info">

                <h2>Thông tin bài thi</h2>

                <div class="result-info-row">
                    <span>Mã đề thi</span>
                    <strong>
                        <asp:Label ID="lblMaDe" runat="server" />
                    </strong>
                </div>

                <div class="result-info-row">
                    <span>Tên đề thi</span>
                    <strong>
                        <asp:Label ID="lblTenDe" runat="server" />
                    </strong>
                </div>

                <div class="result-info-row">
                    <span>Thời gian nộp bài</span>
                    <strong>
                        <asp:Label ID="lblThoiGian" runat="server" />
                    </strong>
                </div>

            </div>

            <div class="result-statistics">

                <h2>Thống kê bài làm</h2>

                <div class="statistics-grid">

                    <div class="statistics-item total">
                        <span>Tổng số câu</span>
                        <strong>
                            <asp:Label ID="lblTongCau" runat="server" />
                        </strong>
                    </div>

                    <div class="statistics-item answered">
                        <span>Đã trả lời</span>
                        <strong>
                            <asp:Label ID="lblDaTraLoi" runat="server" />
                        </strong>
                    </div>

                    <div class="statistics-item correct">
                        <span>Số câu đúng</span>
                        <strong>
                            <asp:Label ID="lblCauDung" runat="server" />
                        </strong>
                    </div>

                    <div class="statistics-item wrong">
                        <span>Số câu sai</span>
                        <strong>
                            <asp:Label ID="lblCauSai" runat="server" />
                        </strong>
                    </div>

                    <div class="statistics-item unanswered">
                        <span>Bỏ trống</span>
                        <strong>
                            <asp:Label ID="lblBoTrong" runat="server" />
                        </strong>
                    </div>

                </div>

            </div>

            <div class="result-actions">

                <asp:HyperLink ID="lnkDanhSachDe"
                    runat="server"
                    NavigateUrl="~/DanhSachDeThi.aspx"
                    CssClass="result-button primary">
                    Làm bài thi khác
                </asp:HyperLink>

                <asp:HyperLink ID="lnkLichSu"
                    runat="server"
                    NavigateUrl="~/LichSuThi.aspx"
                    CssClass="result-button secondary">
                    Xem lịch sử thi
                </asp:HyperLink>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlThongBao"
            runat="server"
            Visible="false"
            CssClass="result-error">

            <asp:Label ID="lblThongBao" runat="server" />

            <div>
                <a href="DanhSachDeThi.aspx">Quay lại danh sách đề thi</a>
            </div>

        </asp:Panel>

    </div>

</asp:Content>
