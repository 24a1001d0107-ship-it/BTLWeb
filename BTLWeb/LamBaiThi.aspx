
<%@ Page Title="Làm bài thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="LamBaiThi.aspx.cs"
    Inherits="BTLWeb.LamBaiThi" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="HeadContent"
    runat="server">

    <link href="CSS/LamBaiThi.css" rel="stylesheet" />
    <script src="JS/LamBaiThi.js"></script>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="doing-page">

        <div class="doing-heading">
            <h1>PHÒNG THI TRỰC TUYẾN</h1>

            <asp:Label ID="lblTenDe"
                runat="server"
                CssClass="doing-exam-name">
            </asp:Label>
        </div>

        <div class="doing-info">

            <div class="doing-info-item">
                <span>Mã đề thi</span>
                <strong>
                    <asp:Label ID="lblMaDe" runat="server" />
                </strong>
            </div>

            <div class="doing-info-item">
                <span>Tổng số câu</span>
                <strong>
                    <asp:Label ID="lblTongCau" runat="server" />
                </strong>
            </div>

            <div class="doing-info-item">
                <span>Thời gian</span>
                <strong>
                    <asp:Label ID="lblThoiGian" runat="server" />
                </strong>
            </div>

            <div class="doing-info-item timer-box">
                <span>Thời gian còn lại</span>
                <strong>
                    <asp:Label ID="demNguoc"
                        runat="server"
                        ClientIDMode="Static">
                    </asp:Label>
                </strong>
            </div>

        </div>

        <div class="question-list">

            <asp:Repeater ID="rptCauHoi"
                runat="server"
                OnItemDataBound="rptCauHoi_ItemDataBound">

                <ItemTemplate>

                    <div class="question-card">

                        <div class="question-title">
                            Câu <%# Container.ItemIndex + 1 %>:
                            <%#: Eval("NoiDung") %>
                        </div>

                        <asp:HiddenField ID="hfMaCauHoi"
                            runat="server"
                            Value='<%# Eval("MaCauHoi") %>' />

                        <asp:RadioButtonList ID="rblDapAn"
                            runat="server"
                            CssClass="answer-list"
                            RepeatDirection="Vertical">
                        </asp:RadioButtonList>

                    </div>

                </ItemTemplate>

            </asp:Repeater>

        </div>

        <div class="submit-area">

            <asp:Label ID="lblThongBao"
                runat="server"
                CssClass="doing-message">
            </asp:Label>

            <asp:Button ID="btnNopBai"
                runat="server"
                Text="NỘP BÀI THI"
                CssClass="submit-exam-button"
                OnClick="btnNopBai_Click"
                OnClientClick="return xacNhanNopBai();" />

        </div>

    </div>

</asp:Content>
