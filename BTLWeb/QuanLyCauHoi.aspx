
<%@ Page Title="Quản lý câu hỏi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="QuanLyCauHoi.aspx.cs"
    Inherits="BTLWeb.QuanLyCauHoi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/QuanLy.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="management-page">

        <div class="management-header">
            <h1>QUẢN LÝ CÂU HỎI</h1>
            <p>Quản lý ngân hàng câu hỏi trắc nghiệm</p>
        </div>

        <div class="management-form">

            <h2>Thông tin câu hỏi</h2>

            <asp:HiddenField ID="hfMaCauHoiCu" runat="server" />

            <div class="management-grid">

                <div class="form-group">
                    <label>Mã câu hỏi</label>
                    <asp:TextBox ID="txtMaCauHoi" runat="server"
                        CssClass="management-input"
                        placeholder="Ví dụ: CH011">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Thuộc đề thi</label>
                    <asp:DropDownList ID="ddlMaDe" runat="server"
                        CssClass="management-input">
                    </asp:DropDownList>
                </div>

                <div class="form-group full-width">
                    <label>Nội dung câu hỏi</label>
                    <asp:TextBox ID="txtNoiDung" runat="server"
                        CssClass="management-input"
                        TextMode="MultiLine"
                        Rows="3"
                        placeholder="Nhập nội dung câu hỏi">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Đáp án A</label>
                    <asp:TextBox ID="txtDapAnA" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập đáp án A">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Đáp án B</label>
                    <asp:TextBox ID="txtDapAnB" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập đáp án B">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Đáp án C</label>
                    <asp:TextBox ID="txtDapAnC" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập đáp án C">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Đáp án D</label>
                    <asp:TextBox ID="txtDapAnD" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập đáp án D">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Đáp án đúng</label>
                    <asp:DropDownList ID="ddlDapAnDung" runat="server"
                        CssClass="management-input">
                        <asp:ListItem Value="A">A</asp:ListItem>
                        <asp:ListItem Value="B">B</asp:ListItem>
                        <asp:ListItem Value="C">C</asp:ListItem>
                        <asp:ListItem Value="D">D</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>

            <div class="management-buttons">

                <asp:Button ID="btnThem" runat="server"
                    Text="Thêm câu hỏi"
                    CssClass="management-button add-button"
                    OnClick="btnThem_Click" />

                <asp:Button ID="btnCapNhat" runat="server"
                    Text="Cập nhật"
                    CssClass="management-button update-button"
                    OnClick="btnCapNhat_Click" />

                <asp:Button ID="btnLamMoi" runat="server"
                    Text="Làm mới"
                    CssClass="management-button reset-button"
                    OnClick="btnLamMoi_Click" />

            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="management-message">
            </asp:Label>

        </div>

        <div class="management-list">

            <h2>Danh sách câu hỏi</h2>

            <div class="management-search">

                <asp:DropDownList ID="ddlLocMaDe" runat="server"
                    CssClass="management-input"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlLocMaDe_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:TextBox ID="txtTimKiem" runat="server"
                    CssClass="management-input"
                    placeholder="Nhập mã hoặc nội dung câu hỏi...">
                </asp:TextBox>

                <asp:Button ID="btnTimKiem" runat="server"
                    Text="Tìm kiếm"
                    CssClass="management-button search-button"
                    OnClick="btnTimKiem_Click" />

                <asp:Button ID="btnHienTatCa" runat="server"
                    Text="Hiện tất cả"
                    CssClass="management-button reset-button"
                    OnClick="btnHienTatCa_Click" />

            </div>

            <div class="management-table-wrapper">

                <asp:GridView ID="gvCauHoi" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="management-table"
                    GridLines="None"
                    DataKeyNames="MaCauHoi"
                    EmptyDataText="Chưa có câu hỏi nào."
                    OnRowCommand="gvCauHoi_RowCommand">

                    <Columns>

                        <asp:BoundField DataField="MaCauHoi"
                            HeaderText="Mã câu hỏi" />

                        <asp:BoundField DataField="MaDe"
                            HeaderText="Mã đề" />

                        <asp:BoundField DataField="NoiDung"
                            HeaderText="Nội dung câu hỏi" />

                        <asp:BoundField DataField="DapAnDung"
                            HeaderText="Đáp án đúng" />

                        <asp:TemplateField HeaderText="Thao tác">
                            <ItemTemplate>

                                <asp:LinkButton ID="btnSua" runat="server"
                                    Text="Sửa"
                                    CommandName="SuaCauHoi"
                                    CommandArgument='<%# Eval("MaCauHoi") %>'
                                    CssClass="table-action edit-action">
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnXoa" runat="server"
                                    Text="Xóa"
                                    CommandName="XoaCauHoi"
                                    CommandArgument='<%# Eval("MaCauHoi") %>'
                                    CssClass="table-action toggle-action"
                                    OnClientClick="return confirm('Bạn có chắc muốn xóa câu hỏi này?');">
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>
