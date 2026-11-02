
<%@ Page Title="Quản lý đề thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="QuanLyDeThi.aspx.cs"
    Inherits="BTLWeb.QuanLyDeThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/QuanLy.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="management-page">

        <div class="management-header">
            <h1>QUẢN LÝ ĐỀ THI</h1>
            <p>Thêm, chỉnh sửa, tìm kiếm và quản lý các đề thi</p>
        </div>

        <div class="management-form">

            <h2>Thông tin đề thi</h2>

            <asp:HiddenField ID="hfMaDeCu" runat="server" />

            <div class="management-grid">

                <div class="form-group">
                    <label>Mã đề thi</label>
                    <asp:TextBox ID="txtMaDe" runat="server"
                        CssClass="management-input"
                        placeholder="Ví dụ: DE007">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Tên đề thi</label>
                    <asp:TextBox ID="txtTenDe" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập tên đề thi">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Môn học</label>
                    <asp:TextBox ID="txtMonHoc" runat="server"
                        CssClass="management-input"
                        placeholder="Nhập tên môn học">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Thời gian làm bài (phút)</label>
                    <asp:TextBox ID="txtThoiGian" runat="server"
                        CssClass="management-input"
                        TextMode="Number">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Số câu hỏi</label>
                    <asp:TextBox ID="txtSoCau" runat="server"
                        CssClass="management-input"
                        TextMode="Number">
                    </asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Trạng thái</label>
                    <asp:DropDownList ID="ddlTrangThai" runat="server"
                        CssClass="management-input">
                        <asp:ListItem Value="1">Đang hoạt động</asp:ListItem>
                        <asp:ListItem Value="0">Đang ẩn</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group full-width">
                    <label>Mô tả đề thi</label>
                    <asp:TextBox ID="txtMoTa" runat="server"
                        CssClass="management-input"
                        TextMode="MultiLine"
                        Rows="3"
                        placeholder="Nhập mô tả đề thi">
                    </asp:TextBox>
                </div>

            </div>

            <div class="management-buttons">

                <asp:Button ID="btnThem" runat="server"
                    Text="Thêm đề thi"
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

            <h2>Danh sách đề thi</h2>

            <div class="management-search">

                <asp:TextBox ID="txtTimKiem" runat="server"
                    CssClass="management-input"
                    placeholder="Nhập mã đề hoặc tên đề thi...">
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

                <asp:GridView ID="gvDeThi" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="management-table"
                    GridLines="None"
                    DataKeyNames="MaDe"
                    EmptyDataText="Chưa có đề thi nào."
                    OnRowCommand="gvDeThi_RowCommand">

                    <Columns>

                        <asp:BoundField DataField="MaDe"
                            HeaderText="Mã đề" />

                        <asp:BoundField DataField="TenDe"
                            HeaderText="Tên đề thi" />

                        <asp:BoundField DataField="MonHoc"
                            HeaderText="Môn học" />

                        <asp:BoundField DataField="ThoiGian"
                            HeaderText="Thời gian (phút)" />

                        <asp:BoundField DataField="SoCau"
                            HeaderText="Số câu" />

                        <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <%# Eval("TrangThai").ToString() == "1"
                                    ? "Đang hoạt động" : "Đang ẩn" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Thao tác">
                            <ItemTemplate>

                                <asp:LinkButton ID="btnSua" runat="server"
                                    Text="Sửa"
                                    CommandName="SuaDe"
                                    CommandArgument='<%# Eval("MaDe") %>'
                                    CssClass="table-action edit-action">
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnAnHien" runat="server"
                                    Text='<%# Eval("TrangThai").ToString() == "1"
                                        ? "Ẩn" : "Mở lại" %>'
                                    CommandName="AnHienDe"
                                    CommandArgument='<%# Eval("MaDe") %>'
                                    CssClass="table-action toggle-action"
                                    OnClientClick="return confirm('Bạn có chắc muốn thay đổi trạng thái đề thi?');">
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>
